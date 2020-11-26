using System;
using System.Collections.Generic;
using UnityEngine;
namespace LevelDesign.PTexture.TSplatmap
{
    public class SplatmapHelper
    {
        public List<SplatHeight> removeSplashHeight(List<SplatHeight> splatHeightsList)
        {
            List<SplatHeight> keepList = new List<SplatHeight>();
            foreach (SplatHeight height in splatHeightsList)
            {
                if (!height.remove)
                {
                    keepList.Add(height);
                }
            }

            return keepList;
        }

        public TerrainLayer GetTerrainLayer(SplatHeight splatHeight)
        {
            TerrainLayer terrainLayer = new TerrainLayer();
            terrainLayer.diffuseTexture = splatHeight.texture;
            terrainLayer.tileOffset = splatHeight.tileOffset;
            terrainLayer.tileSize = splatHeight.tileSize;          
            terrainLayer.diffuseTexture.Apply(true);
            return terrainLayer;
        }


        public void ApplyHeight(TerrainData terrainData, List<SplatHeight> splatHeightsList)
        {
            float[,] heightMap = terrainData.GetHeights(0, 0, 
                                            terrainData.heightmapResolution, terrainData.heightmapResolution);
            float[,,] splatmapData = new float[terrainData.alphamapWidth, 
                                                terrainData.alphamapHeight,
                                                terrainData.alphamapLayers];

            for(int y = 0; y < terrainData.alphamapHeight; y++)
            {
                for (int x = 0; x < terrainData.alphamapWidth; x++)
                {
                    float[] splat = new float[terrainData.alphamapLayers]; 
                    for (int i = 0; i < splatHeightsList.Count; i++)
                    {
                        float noise = Mathf.PerlinNoise(x * splatHeightsList[i].noiseX, y * splatHeightsList[i].noiseY) * splatHeightsList[i].noiseScaler;
                        float offset = splatHeightsList[i].offset + noise;
                        float minHeight = splatHeightsList[i].minHeight - offset;
                        float maxHeight = splatHeightsList[i].maxHeight + offset;
                        float stepness = terrainData.GetSteepness(y / (float)terrainData.alphamapHeight, x / (float)terrainData.alphamapWidth);

                        if (heightMap[x, y] >= minHeight 
                            && heightMap[x, y] <= maxHeight 
                            && stepness >= splatHeightsList[i].minSteepness 
                            && stepness <= splatHeightsList[i].maxSteepness)
                        {
                            splat[i] = 1;
                        }
                    }
                     NormalizeVector(splat);
                    for (int j = 0; j < splatHeightsList.Count; j++)
                    {
                        splatmapData[x, y, j] = splat[j];
                    }
                }
            }
            terrainData.SetAlphamaps(0, 0, splatmapData);
        }

        private void NormalizeVector(float[] splat)
        {
            float total = 0;
            for (int i = 0; i < splat.Length; i++)
            {
                total += splat[i];
            }
            for (int i = 0; i < splat.Length; i++)
            {
                splat[i] /= total ;
            }
            
        }
    }
}
