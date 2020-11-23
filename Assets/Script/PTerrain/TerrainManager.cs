using UnityEngine;
using LevelDesign.PTerrain.TRandom;
using LevelDesign.PTerrain.Common;
using System.Collections.Generic;
using LevelDesign.PTerrain.Perlin;
using LevelDesign.PTerrain.Voronoi;
using LevelDesign.PTerrain.MidPoint;


namespace LevelDesign.PTerrain
{

    [ExecuteInEditMode]
    public class TerrainManager : MonoBehaviour
    {
        //Helper class and model       
        private RandomTerrainHelper randomTerrainHelper;
        private CommonTerrainHelper commonTerrainHelper;
        private PerlinHelper perlinHelper;
        private VoronoiHelper voronoiHelper;
        private MidPointHelper midPointHelper;    

        //Terrain info
        public Terrain terrain;
        public TerrainData terrainData;

        //Random height
        public Vector2 randomHeightRange;
        public Texture2D heighMapImage;
        public Vector3 heightMapScale = new Vector3(1, 1, 1);

        //Perlin data
        public float perlinXscale = 0.01f;
        public float perlinYscale = 0.01f;
        public int perlinXoffset = 1;
        public int perlinYoffset = 1;
        public int perlinOctave = 8;
        public float perlinPersistance;
        public float perlinHeightScale = 0.01f;

        //Voronoi data
        public float voronoiMinHeight = 0.1f;
        public float voronoiMaxHeight = 1f;
        public float voronoiFalloff = 0.2f;
        public float voronoiDropoff = 0.6f;
        public int voronoiPeakCount = 8;

        //Multiple Perlin
        public List<PerlinParameters> perlinList = new List<PerlinParameters>()
        {
            new PerlinParameters()
        };

        //Midpoint data
        public float midPointMinHeight = 0.1f;
        public float midPointMaxHeight = 0.3f;
        public float midPointRoughness = 2.0f;
        public float midPointHeightPower = 2.0f;

        //Smooth
        public int smoothAmount = 1;

        //save
        public string directoryPath = "/Data/HeightMap/Generated/";
        public string filename = "terrain_";


        private void OnEnable()
        {
            Debug.Log("OnEnable TerrainManager");
            initData();
            initHelper();
        }

        private void initHelper()
        {
            randomTerrainHelper = new RandomTerrainHelper();
            commonTerrainHelper = new CommonTerrainHelper();
            perlinHelper = new PerlinHelper();
            voronoiHelper = new VoronoiHelper();
            midPointHelper = new MidPointHelper();           
        }

        private void initData()
        {
            terrain = GetComponent<Terrain>();
            terrainData = Terrain.activeTerrain.terrainData;
        }

        public void GetRandomTerrain()
        {
            randomTerrainHelper.GenerateTerrain(terrainData, randomHeightRange);
        }

        public void ResetTerrain()
        {
            commonTerrainHelper.ResetTerrain(terrainData);
        }

        public void LoadHeightMap()
        {
            if (heighMapImage != null)
            {
                randomTerrainHelper.LoadTerrain(terrainData, heighMapImage, heightMapScale);
            }
        }

        public void GetPerlinTerrain()
        {
            perlinHelper.GetPerlinTerrain(terrainData, perlinXscale, perlinYscale, perlinXoffset, perlinYoffset, perlinOctave, perlinPersistance, perlinHeightScale);
        }


        public void GetMultiplePerlinTerrain()
        {
            perlinHelper.GetMultiplePerlinTerrain(terrainData, perlinList);
        }


        public void addPerlin()
        {
            perlinList.Add(new PerlinParameters());
        }

        public void removePerlin()
        {
            if (perlinList.Count > 0)
            {
                perlinList = perlinHelper.removePerlin(perlinList);
            }
        }

        public void GetVoronoiTerrain()
        {
            voronoiHelper.GetVoronoiTerrain(terrainData, voronoiMinHeight, voronoiMaxHeight, voronoiFalloff, voronoiDropoff, voronoiPeakCount);
        }

        public void GetMidPointTerrain()
        {
            midPointHelper.GetMidPointTerrain(terrainData, midPointMinHeight, midPointMaxHeight, midPointRoughness, midPointHeightPower);
        }

        public void SmoothTerrain()
        {
            commonTerrainHelper.SmoothTerrain(terrainData, smoothAmount);
        }


        public void SaveTerrainHeight()
        {
            commonTerrainHelper.SaveHeightMap(terrainData, directoryPath, filename);
        }
    }
}
