using UnityEngine;
using System.Collections.Generic;
using LevelDesign.PVegitation.PTree;


namespace LevelDesign.PVegitation
{

    [ExecuteInEditMode]
    public class VegitationManager : MonoBehaviour
    {
       

        //Terrain info
        public Terrain terrain;
        public TerrainData terrainData;

        private VegitationHelper VegitationHelper;

        //Vegitation    
        public int maxTrees = 5000;
        public int treeSpacing = 5;
        public List<Vegitation> vegitationList = new List<Vegitation>()
        {
            new Vegitation()
        };


        private void OnEnable()
        {
            Debug.Log("OnEnable TextureManager");
            initData();
            initHelper();           
        }

        private void initHelper()
        {
            VegitationHelper = new VegitationHelper();
        }

        private void initData()
        {
            terrain = GetComponent<Terrain>();
            terrainData = Terrain.activeTerrain.terrainData;
        }

        public void AddVegitation()
        {
            vegitationList.Add(new Vegitation());
        }

        public void RemoveVegitation()
        {
            if (vegitationList.Count > 0)
            {
                vegitationList = VegitationHelper.RemoveVegitation(vegitationList);
            }
        }

        public void PlantTree()
        {
            VegitationHelper.plantTree(terrainData,vegitationList,treeSpacing,maxTrees);
        }
    }
}
