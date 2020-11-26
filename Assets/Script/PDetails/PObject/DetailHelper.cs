using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.PDetails.PObject
{
    public class DetailHelper
    {

        private TerrainData terrainData;
        private List<Details> detailsList;      
        private int detailSpacing;

        public DetailHelper(TerrainData terrainData)
        {
            this.terrainData = terrainData;
        }

        public void SetDetailsList(List<Details> detailsList)
        {
            this.detailsList = detailsList;
        }

   

        public void SetDetailSpacing(int detailSpacing)
        {
            this.detailSpacing = detailSpacing;
        }


        public List<Details> RemoveDetails()
        {
            List<Details> keepList = new List<Details>();
            foreach (Details details in detailsList)
            {
                if (!details.remove)
                {
                    keepList.Add(details);
                }
            }
            return keepList;
        }

        public void AddDetails()
        {
            GenerateDetails();
           GenerateDetailMap();           
        }

        private void GenerateDetailMap()
        {
            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            for (int i = 0; i < terrainData.detailPrototypes.Length; i++)
            {
                int[,] detailMap = new int[terrainData.detailWidth, terrainData.detailHeight];
                for (int y = 0; y < terrainData.detailHeight; y += detailSpacing)
                {
                    for (int x = 0; x < terrainData.detailWidth; x += detailSpacing)
                    {
                        if (Random.Range(0f, 1f) > detailsList[i].density)
                        {
                            continue;
                        }
                      

                        float noise = Normalize(Mathf.PerlinNoise(x * detailsList[i].feather,
                                                        y * detailsList[i].feather), 0f, 1f, 0.5f, 1f);
                          float minHeight = detailsList[i].minHeight * noise - detailsList[i].overlap * noise;
                          float maxHeight = detailsList[i].minHeight * noise + detailsList[i].overlap * noise;
                        float height = terrainData.GetHeight(x, y) / terrainData.size.y;
                        /*float steepness = terrainData.GetSteepness(xHM / (float)terrainData.size.x,
                                                                     yHM / (float)terrainData.size.z);*/

                        float steepness = terrainData.GetSteepness(x / (float)terrainData.size.x,
                                                                  y / (float)terrainData.size.z);
                        if (//height >= minHeight && height <= maxHeight  && 
                            steepness >= detailsList[i].minSteepness
                               && steepness <= detailsList[i].maxSteepness
                             )
                        {
                            detailMap[y, x] = 1;
                        }
                        //detailMap[y, x] = 1;
                    }
                }
                terrainData.SetDetailLayer(0, 0, i, detailMap);
            }
        }

        private void GenerateDetails()
        {
            DetailPrototype[] detailPrototypes = new DetailPrototype[detailsList.Count];
            int index = 0;
            foreach (var detail in detailsList)
            {
                detailPrototypes[index] = new DetailPrototype();
                detailPrototypes[index].prototype = detail.prototype;
                detailPrototypes[index].prototypeTexture = detail.prototypeTexture;
                detailPrototypes[index].healthyColor = detail.healthyColor;
                detailPrototypes[index].dryColor = detail.dryColor;
                detailPrototypes[index].minHeight = detail.heightRange.x;
                detailPrototypes[index].maxHeight = detail.heightRange.y;
                detailPrototypes[index].minWidth = detail.widthRange.x;
                detailPrototypes[index].maxWidth = detail.widthRange.y;
                detailPrototypes[index].noiseSpread = detail.noiseSpread;
                if (detailPrototypes[index].prototype)
                {
                    detailPrototypes[index].usePrototypeMesh = true;
                    detailPrototypes[index].renderMode = DetailRenderMode.VertexLit;
                }
                else
                {
                    detailPrototypes[index].usePrototypeMesh = false;
                    detailPrototypes[index].renderMode = DetailRenderMode.GrassBillboard;
                }
                index++;
            }
            terrainData.detailPrototypes = detailPrototypes;
        }

        private float Normalize(float value, float originalMin, float originalMax, float targetMin, float targetMax)
        {
            return (value - originalMin) * (targetMax - targetMin) / (originalMax - originalMin) + targetMin;
        }
    }   

}
