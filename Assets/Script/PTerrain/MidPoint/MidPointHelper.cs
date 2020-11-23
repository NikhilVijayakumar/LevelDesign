
using UnityEngine;

namespace LevelDesign.PTerrain.MidPoint
{
    public class MidPointHelper 
    {
        public void GetMidPointTerrain(TerrainData terrainData, float midPointMinHeight, float midPointMaxHeight, float midPointRoughness, float midPointHeightPower)
        {

            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            int width = terrainData.heightmapResolution - 1;
            int squareSize = width;

            int cornerX, cornerY, midX, midY, pmidXL, pmidXR, pmidYU, pmidYD;

            float heightDampner =(float) Mathf.Pow(midPointHeightPower, -1 * midPointRoughness);


            heightMap[0, 0] = Random.Range(0, 0.2f);
            heightMap[0, terrainData.heightmapResolution-2] = Random.Range(0, 0.2f);
            heightMap[terrainData.heightmapResolution-2, 0] = Random.Range(0, 0.2f);
            heightMap[terrainData.heightmapResolution-2, terrainData.heightmapResolution-1] = Random.Range(0, 0.2f);
            while (squareSize > 0)
            {

                for (int x = 0; x < width; x += squareSize)
                {
                    for (int y = 0; y < width; y += squareSize)
                    {
                        cornerX = x + squareSize;
                        cornerY = y + squareSize;
                        midX = (int)(x + squareSize / 2);
                        midY = (int)(y + squareSize / 2);
                        heightMap[midX, midY] = (float)((heightMap[x, y] +
                                                                    heightMap[x, cornerY] +
                                                                    heightMap[cornerX, y] +
                                                                    heightMap[cornerX, cornerY]) /
                                                                     4.0f) +
                                                                     Random.Range(midPointMinHeight, midPointMaxHeight);                                                               
                                                                     
                    }
                }

                for (int x = 0; x < width; x += squareSize)
                {
                    for (int y = 0; y < width; y += squareSize)
                    {
                        cornerX = x + squareSize;
                        cornerY = y + squareSize;
                        midX = (int)(x + squareSize / 2);
                        midY = (int)(y + squareSize / 2);
                        pmidXL = (int)(midX - squareSize);
                        pmidXR = (int)(midX + squareSize);
                        pmidYU = (int)(midY + squareSize);
                        pmidYD = (int)(midY - squareSize);

                        if(pmidXL <=0 || pmidYD <=0 
                            ||pmidXR >width-1 || pmidYU >= width - 1)
                        {
                            continue;
                        }

                        heightMap[midX, y] = (float)((heightMap[midX, midY] +
                                                                   heightMap[x, y] +
                                                                   heightMap[midX, pmidYD] +
                                                                   heightMap[cornerX, y]) /
                                                                    4.0f) +
                                                                     Random.Range(midPointMinHeight, midPointMaxHeight);

                        heightMap[midX, cornerY] = (float)((heightMap[x, cornerY] +
                                                               heightMap[midX, midY] +
                                                               heightMap[cornerX, cornerY] +
                                                               heightMap[midX, pmidYU]) /
                                                                4.0f) +
                                                                Random.Range(midPointMinHeight, midPointMaxHeight);


                        heightMap[x, midY] = (float)((heightMap[x, y] +
                                                               heightMap[pmidXL, midY] +
                                                               heightMap[x, cornerY] +
                                                               heightMap[midX, midY]) /
                                                                4.0f) +
                                                                 Random.Range(midPointMinHeight, midPointMaxHeight);

                        heightMap[cornerX, midY] = (float)((heightMap[cornerX, y] +
                                                               heightMap[midX, midY] +
                                                               heightMap[cornerX, cornerY] +
                                                               heightMap[pmidXR, midY]) /
                                                                4.0f) +
                                                                Random.Range(midPointMinHeight, midPointMaxHeight);

                    }
                }


                squareSize = (int)(squareSize / 2.0f);
                midPointMinHeight *= heightDampner;
                midPointMaxHeight *= heightDampner;
            }

           
            terrainData.SetHeights(0, 0, heightMap);
        }
    }
}
