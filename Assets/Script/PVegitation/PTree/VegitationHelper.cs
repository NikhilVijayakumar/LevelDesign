

using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.PVegitation.PTree
{
    public class VegitationHelper
    {

        private TerrainData terrainData;
        private List<Vegitation> vegitationList;
        private int treeSpacing;
        private int maxTrees;
        private int terrainLayer;
        private Vector3 terrainPosition;

        public VegitationHelper(TerrainData terrainData)
        {
            this.terrainData = terrainData;
        } 
        
        public void SetVegitationList( List<Vegitation> vegitationList)
        {
            this.vegitationList = vegitationList;
        }

        public void SetTreeSpacing( int treeSpacing)
        {
            this.treeSpacing = treeSpacing;            
        }

        public void SetMaxTrees(int maxTrees)
        {
           this.maxTrees = maxTrees;
        }

        public void SetTerrainLayer(int terrainLayer)
        {
            this.terrainLayer = terrainLayer;
        }

        public void SetTerrainPosition( Vector3 terrainPosition)
        {
            this.terrainPosition = terrainPosition;
        }

        public List<Vegitation> RemoveVegitation()
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

        public void plantTree()
        {          
            TreePrototype[] treePrototypeList = GenerateTreePrototype();
            terrainData.treePrototypes = treePrototypeList;            
            List<TreeInstance> treeInstanceList = GenerateTreeInstances();           
            terrainData.treeInstances = treeInstanceList.ToArray();    
        }

        private List<TreeInstance> GenerateTreeInstances()
        {
            List<TreeInstance> treeInstanceList = new List<TreeInstance>();
           
            for (int z = 0; z < terrainData.size.z; z+= treeSpacing)
            {
                for (int x = 0; x < terrainData.size.x; x+=treeSpacing)
                {
                    for (int i = 0; i < vegitationList.Count; i++)
                    {   
                        if(Random.Range(0f,1f) > vegitationList[i].density)
                        {
                            break;
                        }

                        float height = terrainData.GetHeight(x, z)/terrainData.size.y;
                        float minHeight = vegitationList[i].minHeight;
                        float maxHeight = vegitationList[i].maxHeight;

                        float steepness = terrainData.GetSteepness(x / (float)terrainData.size.x,
                                                                      z / (float)terrainData.size.z);
                        float minSteepness = vegitationList[i].minSteepness;
                        float maxSteepness = vegitationList[i].maxSteepness;

                        if (height>=minHeight && height <= maxHeight
                            && steepness >= minSteepness && steepness <= maxSteepness)
                        {
                            TreeInstance instance = new TreeInstance();
                            instance.position = new Vector3((x ) / terrainData.size.x,
                                                            height,
                                                            (z ) / terrainData.size.z);
                            Vector3 treeWorldPostion = new Vector3(instance.position.x * terrainData.size.x,
                                                                    instance.position.y * terrainData.size.y,
                                                                    instance.position.z * terrainData.size.z);
                            RaycastHit hit;
                            Vector3 offset = new Vector3(0, 10, 0);
                            int layerMask = 1 << terrainLayer;
                            if(Physics.Raycast(treeWorldPostion + offset, -Vector3.up,out hit, 100, layerMask) 
                                || Physics.Raycast(treeWorldPostion - offset, Vector3.up, out hit, 100, layerMask))
                            {
                                float treeHeight = (hit.point.y - terrainPosition.y) / terrainData.size.y;
                                instance.position = new Vector3(instance.position.x * terrainData.size.x/terrainData.alphamapWidth,
                                                            treeHeight,
                                                            instance.position.z * terrainData.size.z/terrainData.alphamapWidth);

                                instance.rotation = Random.Range(vegitationList[i].minRotation, vegitationList[i].maxRotation);
                                instance.prototypeIndex = i;
                                instance.color =Color.Lerp(vegitationList[i].color1, 
                                                         vegitationList[i].color2,
                                                         Random.Range(0f,1f));
                                instance.lightmapColor = vegitationList[i].lightColor;
                                float scale = Random.Range(vegitationList[i].minScale, vegitationList[i].maxScale);
                                instance.heightScale = scale;
                                instance.widthScale = scale;
                                treeInstanceList.Add(instance);
                                if (treeInstanceList.Count >= maxTrees)
                                {
                                    return treeInstanceList;
                                }
                            }                           
                        }   
                    }
                }
            }
            return treeInstanceList;
        }

        private TreePrototype[] GenerateTreePrototype()
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
