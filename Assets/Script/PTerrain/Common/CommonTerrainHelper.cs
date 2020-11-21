using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.PTerrain.Common
{
    public class CommonTerrainHelper
    {


        public void ResetTerrain(TerrainData terrainData)
        {

            float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
            for (int x = 0; x < terrainData.heightmapResolution; x++)
            {
                for (int y = 0; y < terrainData.heightmapResolution; y++)
                {
                    heightMap[x, y] = 0;
                }
            }

            terrainData.SetHeights(0, 0, heightMap);

        }

      
    }
}



