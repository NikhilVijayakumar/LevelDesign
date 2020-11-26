using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.PTerrain.Perlin
{
    public class PerlinHelper
    {

        public float fBM(float x, float y, int oct, float persistance, int offsetX, int offsetY)
        {
            float total = 0;
            float frequency = 1;
            float amplitude = 1;
            float maxValue = 0;
            for (int i = 0; i < oct; i++)
            {
                total += Mathf.PerlinNoise((x + offsetX) *
                    frequency, (y + offsetY) * frequency) * amplitude;
                maxValue += amplitude;
                amplitude *= persistance;
                frequency *= 2;
            }
            return total / maxValue;
        }

        public void GetPerlinTerrain(TerrainData terrainData, float perlinXscale,float perlinYscale, int perlinXoffset, int perlinYoffset,
                                        int perlinOctave, float perlinPersistance, float perlinHeightScale)
        {

            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            for (int x = 0; x < terrainData.heightmapResolution; x++)
            {
                for (int y = 0; y < terrainData.heightmapResolution; y++)
                {
                    heightMap[x, y] += fBM((x + perlinXoffset) * perlinXscale, (y + perlinYoffset) * perlinYscale,
                                            perlinOctave, perlinPersistance,
                                            perlinXoffset, perlinYoffset) * perlinHeightScale;
                }
            }

            terrainData.SetHeights(0, 0, heightMap);
        }

  



        public void GetMultiplePerlinTerrain(TerrainData terrainData,List<PerlinParameters> perlinList)
        {

            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            for (int x = 0; x < terrainData.heightmapResolution; x++)
            {
                for (int y = 0; y < terrainData.heightmapResolution; y++)
                {
                   foreach(PerlinParameters perlinParameters in perlinList)
                    {
                        heightMap[x, y] += fBM((x + perlinParameters.perlinXoffset) * perlinParameters.perlinXscale, (y + perlinParameters.perlinYoffset) * perlinParameters.perlinYscale,
                                           perlinParameters.perlinOctave, perlinParameters.perlinPersistance,
                                           perlinParameters.perlinXoffset, perlinParameters.perlinYoffset) * perlinParameters.perlinHeightScale;
                    }
                }
            }

            terrainData.SetHeights(0, 0, heightMap);
        }

        public List<PerlinParameters> RemovePerlin(List<PerlinParameters> perlinList)
        {
            List<PerlinParameters> keepList = new List<PerlinParameters>();
            foreach (PerlinParameters perlinParameters in perlinList)
            {
                if (!perlinParameters.remove)
                {
                    keepList.Add(perlinParameters);
                }
            }

            return keepList;
        }

    }
}
