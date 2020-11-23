

using UnityEngine;

namespace LevelDesign.PTerrain.Voronoi
{
    public class VoronoiHelper
    {
        public void GetVoronoiTerrain(TerrainData terrainData,float voronoiMinHeight, float voronoiMaxHeight, float voronoiFalloff, float voronoiDropoff, int voronoiPeakCount)
        {

            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            for (int i = 0; i < voronoiPeakCount; i++)
            {
               
                Vector3 peak = new Vector3(Random.Range(0, terrainData.heightmapResolution),
                                           Random.Range(voronoiMinHeight, voronoiMaxHeight),
                                             Random.Range(0, terrainData.heightmapResolution));
                if (heightMap[(int)peak.x, (int)peak.z] < peak.y)
                {
                    heightMap[(int)peak.x, (int)peak.z] = peak.y;
                }
                else
                {
                    continue;
                }
                

                Vector2 peakLocation = new Vector2(peak.x, peak.z);
                float maxDistance = Vector2.Distance(new Vector2(0, 0), new Vector2(terrainData.heightmapResolution, terrainData.heightmapResolution));

                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    for (int y = 0; y < terrainData.heightmapResolution; y++)
                    {
                        if (x != peak.x && y != peak.z)
                        {
                            float distanceToPeak = Vector2.Distance(peakLocation, new Vector2(x, y)) * voronoiFalloff / maxDistance;
                            float height = peak.y - distanceToPeak - Mathf.Pow(distanceToPeak, voronoiDropoff);
                            if (heightMap[x, y] < height)
                            {
                                heightMap[x, y] = height;
                            }
                            
                        }
                    }
                }               
            }
            terrainData.SetHeights(0, 0, heightMap);
        }

    }
}

