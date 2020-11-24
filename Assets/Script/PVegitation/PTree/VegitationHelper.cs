

using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.PVegitation.PTree
{
    public class VegitationHelper
    {
        public List<Vegitation> RemoveVegitation(List<Vegitation> vegitationList)
        {
            List<Vegitation> keepList = new List<Vegitation>();
            foreach (Vegitation vegitation in vegitationList)
            {
                if (!vegitation.remove)
                {
                    keepList.Add(vegitation);
                }
            }

            return keepList;
        }

        public void plantTree(TerrainData terrainData, List<Vegitation> vegitationList, int treeSpacing, int maxTrees)
        {
            TreePrototype[] treePrototypeList = GenerateTreePrototype(terrainData,vegitationList);
            terrainData.treePrototypes = treePrototypeList;
            List<TreeInstance> treeInstanceList = GenerateTreeInstances(terrainData, vegitationList, treeSpacing, maxTrees);
            terrainData.treeInstances = treeInstanceList.ToArray();
        }

        private List<TreeInstance> GenerateTreeInstances(TerrainData terrainData, List<Vegitation> vegitationList, int treeSpacing, int maxTrees)
        {
            List<TreeInstance> treeInstanceList = new List<TreeInstance>();
           
            for (int z = 0; z < terrainData.size.z; z+= treeSpacing)
            {
                for (int x = 0; x < terrainData.size.x; x+=treeSpacing)
                {
                    for (int i = 0; i < terrainData.treePrototypes.Length; i++)
                    {
                        float height = terrainData.GetHeight(x, z)/terrainData.size.y;
                        float minHeight = vegitationList[i].minHeight;
                        float maxHeight = vegitationList[i].minHeight;
                        if(height>=minHeight && height <= maxHeight)
                        {
                            TreeInstance instance = new TreeInstance();
                            instance.position = new Vector3((x + Random.Range(-5.0f, 5.0f)) / terrainData.size.x,
                                                            height,
                                                            (z + Random.Range(-5.0f, 5.0f)) / terrainData.size.z);
                            instance.rotation = Random.Range(0f, 360f);
                            instance.prototypeIndex = i;
                            instance.color = Color.white;
                            instance.lightmapColor = Color.white;
                            instance.heightScale = 0.95f;
                            instance.widthScale = 0.95f;
                            treeInstanceList.Add(instance);
                            if (treeInstanceList.Count >= maxTrees)
                            {
                                return treeInstanceList;
                            }
                        }
                        
                    }
                }
            }
            return treeInstanceList;
        }

        private TreePrototype[] GenerateTreePrototype(TerrainData terrainData, List<Vegitation> vegitationList)
        {
            TreePrototype[] treePrototypeList = new TreePrototype[vegitationList.Count];
            int index = 0;
            foreach (var vegitation in vegitationList)
            {
                treePrototypeList[index] = new TreePrototype();
                treePrototypeList[index].prefab = vegitation.mesh;
                index++;
            }
            return treePrototypeList;
        }
    }
}
