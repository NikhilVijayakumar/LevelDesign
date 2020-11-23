using UnityEngine;

namespace LevelDesign.PTerrain.TRandom
{
    public class RandomTerrainHelper
    {        
        public void GenerateTerrain(TerrainData terrainData, Vector2 randomHeightRange)
        {

            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);              
            for(int x =0; x < terrainData.heightmapResolution; x++)
            {
                for(int y=0; y < terrainData.heightmapResolution; y++)
                {
                    heightMap[x, y] += Random.Range(randomHeightRange.x, randomHeightRange.y);
                }
            }

            terrainData.SetHeights(0, 0, heightMap);
        }

        public void LoadTerrain(TerrainData terrainData, Texture2D heighMapImage, Vector3 heightMapScale)
        {

            float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
            for (int x = 0; x < terrainData.heightmapResolution; x++)
            {
                for (int y = 0; y < terrainData.heightmapResolution; y++)
                {
                    heightMap[x, y] = heighMapImage.GetPixel((int)(x * heightMapScale.x),
                                                                (int)(y * heightMapScale.z)).grayscale * heightMapScale.y;
                }
            }

            terrainData.SetHeights(0, 0, heightMap);

        }

    }
}