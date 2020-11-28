using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

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

        public void SmoothTerrain(TerrainData terrainData,int smoothAmount)
        {
            int width = terrainData.heightmapResolution;          
            float[,] heightMap = terrainData.GetHeights(0, 0,
                                           terrainData.heightmapResolution, terrainData.heightmapResolution);
            int smoothProgress = 0;           
             for(int i = 0; i < smoothAmount; i++)
            {
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                    
                    {
                        float avgHeight = heightMap[x, y];
                        List<Vector2> neighbourList = GenerateNeighbours(new Vector2(x, y), width);
                        foreach (Vector2 neighbour in neighbourList)
                        {
                            avgHeight += heightMap[(int)neighbour.x, (int)neighbour.y];
                        }
                         avgHeight /= neighbourList.Count + 1f;
                        heightMap[x, y] = avgHeight;                   
                    }
                }
               smoothProgress++;
               EditorUtility.DisplayProgressBar("Smoothing Terrain", "Loading", smoothProgress/ smoothAmount);
            }
            terrainData.SetHeights(0, 0, heightMap);        
            EditorUtility.ClearProgressBar();
        }

        public List<Vector2> GenerateNeighbours(Vector2 pos,int width)
        {
            List<Vector2> neighbourList = new List<Vector2>();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if(!(x == 0 && y == 0))
                    {
                        Vector2 nPos = new Vector2(Mathf.Clamp(pos.x + x, 0, width - 1),
                            Mathf.Clamp(pos.y + y, 0, width - 1));
                        if (!neighbourList.Contains(nPos))
                        {
                            neighbourList.Add(nPos);
                        }
                    }
                }
            }
            return neighbourList;
        }
       


        public void SaveHeightMap(TerrainData terrainData, string directoryPath,string filename)
        {
            byte[] myBytes;
            int myIndex = 0;
            Texture2D duplicateHeightMap = new Texture2D(terrainData.heightmapResolution, terrainData.heightmapResolution, TextureFormat.ARGB32, false);
            float[,] rawHeights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            
            /// run through the array row by row
            for (int y = 0; y < duplicateHeightMap.height; y++)
            {
                for (int x = 0; x < duplicateHeightMap.width; x++)
                {
                    /// for wach pixel set RGB to the same so it's gray
                    var color = new Vector4(rawHeights[x, y], rawHeights[x, y], rawHeights[x, y], 1);
                    duplicateHeightMap.SetPixel(x, y, color);
                    myIndex++;
                }
            }
            // Apply all SetPixel calls
            duplicateHeightMap.Apply();
            filename += Guid.NewGuid() + ".PNG";
            myBytes = duplicateHeightMap.EncodeToPNG();
            string name = Application.dataPath + directoryPath + filename;
            File.WriteAllBytes(name, myBytes);
            AssetDatabase.Refresh();

        }

        public List<Vector2> GenerateNeighbours(Vector2 pos, int width, int height)
        {
            List<Vector2> neighbours = new List<Vector2>();
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (!(x == 0 && y == 0))
                    {
                        Vector2 nPos = new Vector2(Mathf.Clamp(pos.x + x, 0, width - 1),
                                                    Mathf.Clamp(pos.y + y, 0, height - 1));
                        if (!neighbours.Contains(nPos))
                        {
                            neighbours.Add(nPos);
                        }

                    }
                }
            }
            return neighbours;
        }

        public  System.Random r = new System.Random();
        public  void Shuffle(List<Vector2> list)
        {
            for (int i = list.Count-1; i > 1; i--)
            {
                int k = r.Next(i + 1);
                Vector2 value = list[k];
                list[k] = list[i];
                list[i] = value;
            }
        }

    }
}



