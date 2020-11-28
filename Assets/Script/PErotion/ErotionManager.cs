using UnityEngine;
using LevelDesign.PTerrain.Common;
using System.Collections.Generic;

namespace LevelDesign.PErotion
{

    [ExecuteInEditMode]
    public class ErotionManager : MonoBehaviour
    {
        //Terrain info
        public Terrain terrain;
        public TerrainData terrainData;     

        //Erotionetails        
        public ErosionType erosionType = ErosionType.Rain;
        public float erosionStrength = 0.1f;
        public float erosionAmount = 0.001f;
        public int springPerRiver = 5;
        public float solubility = 0.01f;
        public int droplets = 10;
        public int erosionSmoothAmount = 5;
        public float waterHeight = 0f;
        public float windDirection = 35f;

        private CommonTerrainHelper helper;
        private float[,] tempHeightMap;

        private void OnEnable()
        {
            Debug.Log("OnEnable TextureManager");
            initData();          
        }
        private void initData()
        {
            terrain = GetComponent<Terrain>();
            terrainData = Terrain.activeTerrain.terrainData;
            helper = new CommonTerrainHelper();
        }

        public void Erode()
        {
            switch ((int)erosionType)
            {
                case (int)ErosionType.Rain:
                    Rain();
                    break;
                case (int)ErosionType.Tidal:
                    Tidal();
                    break;
                case (int)ErosionType.Thermal:
                    Thermal();
                    break;
                case (int)ErosionType.River:
                    River();
                    break;
                case (int)ErosionType.Wind:
                    Wind();
                    break;
                case (int)ErosionType.Canyon:
                    Canyon();
                   break;
                    
            }
            helper.SmoothTerrain(terrainData, erosionSmoothAmount);
        }
       
        private void Canyon()
        {
            float digDepth = 0.05f;
            float blankSlope = 0.001f;
            float maxDepth = 0f;
            tempHeightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution,
                                         terrainData.heightmapResolution);
            int cx = 1;
            int cy = Random.Range(10, terrainData.heightmapResolution - 10);
            while (cy>=0 && cy < terrainData.heightmapResolution 
                    && cx >= 0 && cx < terrainData.heightmapResolution)
            {
                CanyonCrawler(cx, cy, tempHeightMap[cx, cy] - digDepth, blankSlope, maxDepth);
                cx += Random.Range(1,3);
                cy += Random.Range(-2, 3);
            }
            terrainData.SetHeights(0, 0, tempHeightMap);
        }

        private void CanyonCrawler(int x, int y, float height, float slope, float maxDepth)
        {
           if(x < 0 || x >= terrainData.heightmapResolution)
            {
                return;
            }

            if (y < 0 || y >= terrainData.heightmapResolution)
            {
                return;
            }

            if (height <= maxDepth)
            {
                return;
            }

            if (tempHeightMap[x,y] <= height)
            {
                return;
            }
            tempHeightMap[x, y] = height;

            CanyonCrawler(x +1 , y, height + Random.Range(slope,slope+0.01f), slope, maxDepth);
            CanyonCrawler(x - 1, y, height + Random.Range(slope, slope + 0.01f), slope, maxDepth);
            CanyonCrawler(x + 1, y + 1, height + Random.Range(slope, slope + 0.01f), slope, maxDepth);
            CanyonCrawler(x - 1, y + 1, height + Random.Range(slope, slope + 0.01f), slope, maxDepth);
            CanyonCrawler(x , y + 1, height + Random.Range(slope, slope + 0.01f), slope, maxDepth);
            CanyonCrawler(x , y + 1, height + Random.Range(slope, slope + 0.01f), slope, maxDepth);
        }

        private void Wind()
        {
            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution,
                                           terrainData.heightmapResolution);
            int width = terrainData.heightmapResolution;
            int height = terrainData.heightmapResolution;
            float sinAngle = -Mathf.Sin(Mathf.Deg2Rad * windDirection);
            float cosAngle = Mathf.Cos(Mathf.Deg2Rad * windDirection);
            for (int y = (int)(-(height - 1) * 1.2f); y <= height * 1.2; y += 10)
            {
                for (int x = (int)(-(width - 1) * 1.2f); x <= width * 1.2; x++)
                {
                    float noise = Mathf.PerlinNoise(x * 0.06f, y * 0.06f) * 20 * erosionStrength;
                    int nx = x;
                    int digy = y + (int)noise;
                    int ny = y + 5 + (int) noise;

                    Vector2 digCoords = new Vector2(x * cosAngle - digy * sinAngle, digy * cosAngle + x * sinAngle);
                    Vector2 pileCoords = new Vector2(nx * cosAngle - ny * sinAngle, ny * cosAngle + nx * sinAngle);

                    if (!(digCoords.x < 0 || digCoords.x > (width -1) || digCoords.y < 0 || digCoords.y > (height - 1)
                        || pileCoords.x < 0 || pileCoords.x > (width - 1) || pileCoords.y < 0 || pileCoords.y > (height - 1)))
                    {
                        heightMap[(int)digCoords.x, (int)digCoords.y] -= 0.001f;
                        heightMap[(int)pileCoords.x, (int)pileCoords.y] -= 0.001f;
                    }
                }
            }
            terrainData.SetHeights(0, 0, heightMap);
        }

        private float[,] RunRiver(Vector2 dropletPostion, float[,] heightMap, float[,] erotionMap)
        {
            int width = terrainData.heightmapResolution;
            int height = terrainData.heightmapResolution;
            while (erotionMap[(int)dropletPostion.x,(int)dropletPostion.y]>0)
            {
                List<Vector2> neighbourList = helper.GenerateNeighbours(dropletPostion, width, height);
                helper.Shuffle(neighbourList);
                bool foundLower = false;
                foreach (var n in neighbourList)
                {
                    if (heightMap[(int)n.x, (int)n.y] < heightMap[(int)dropletPostion.x, (int)dropletPostion.y])
                    {
                        erotionMap[(int)n.x, (int)n.y] = erotionMap[(int)dropletPostion.x,
                                                                (int)dropletPostion.y] - solubility;
                        dropletPostion = n;
                        foundLower = true;
                        break;
                    }
                }
                if (!foundLower)
                {
                    erotionMap[(int)dropletPostion.x, (int)dropletPostion.y] -= solubility;
                }
            }
            return erotionMap;
        }

        private void River()
        {
            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution,
                                            terrainData.heightmapResolution);
            float[,] erotionMap = new float[terrainData.heightmapResolution,
                                          terrainData.heightmapResolution];
            for (int i = 0; i < droplets; i++)
            {
                Vector2 dropletPostion = new Vector2(Random.Range(0, terrainData.heightmapResolution),
                                                Random.Range(0, terrainData.heightmapResolution));
                erotionMap[(int)dropletPostion.x, (int)dropletPostion.y] = erosionStrength;
                for (int j = 0; j < springPerRiver; j++)
                {
                    erotionMap = RunRiver(dropletPostion, heightMap, erotionMap);
                }
            }
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    if (erotionMap[x, y] > 0)
                    {
                        heightMap[x, y] -= erotionMap[x, y];
                    }
                }
            }
            terrainData.SetHeights(0, 0, heightMap);
        }

        private void Thermal()
        {
            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution,
                                          terrainData.heightmapResolution);
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    Vector2 location = new Vector2(x, y);
                    List<Vector2> neighbourList = helper.GenerateNeighbours(location,
                        terrainData.heightmapResolution,
                        terrainData.heightmapResolution);
                    foreach (var n in neighbourList)
                    {
                        if(heightMap[x,y] > heightMap[(int)n.x, (int)n.y] + erosionStrength)
                        {
                            float currentHeight = heightMap[x, y];
                            heightMap[x, y] = currentHeight * erosionAmount;
                            heightMap[(int)n.x, (int)n.y] += currentHeight * erosionAmount;
                        }
                        
                    }
                }
            }
            terrainData.SetHeights(0, 0, heightMap);
        }

        private void Tidal()
        {
            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution,
                                          terrainData.heightmapResolution);
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    Vector2 location = new Vector2(x, y);
                    List<Vector2> neighbourList = helper.GenerateNeighbours(location,
                        terrainData.heightmapResolution,
                        terrainData.heightmapResolution);
                    foreach (var n in neighbourList)
                    {
                        if (heightMap[x, y] < waterHeight && 
                            heightMap[(int)n.x, (int)n.y] > waterHeight)
                        {                            
                            heightMap[x, y] = waterHeight;
                            heightMap[(int)n.x, (int)n.y] = waterHeight;
                        }

                    }
                }
            }
            terrainData.SetHeights(0, 0, heightMap);
        }

        private void Rain()
        {
            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, 
                                            terrainData.heightmapResolution);
            for (int i = 0; i < droplets; i++)
            {
                int x = Random.Range(0, terrainData.heightmapResolution);
                int y = Random.Range(0, terrainData.heightmapResolution);
                heightMap[x, y] -= erosionStrength; 
            }
            terrainData.SetHeights(0, 0, heightMap);
        }
    }
}
