using UnityEngine;
using System.Collections.Generic;
using LevelDesign.PVegitation.PTree;
using LevelDesign.PLayer;

namespace LevelDesign.PVegitation
{

    [ExecuteInEditMode]
    public class VegitationManager : MonoBehaviour
    {
       

        //Terrain info
        public Terrain terrain;
        public TerrainData terrainData;

        private VegitationHelper VegitationHelper;
        private int terrainLayer = -1;

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
            VegitationHelper = new VegitationHelper(terrainData);
        }

        private void initData()
        {
            terrain = GetComponent<Terrain>();
            terrainData = Terrain.activeTerrain.terrainData;
            terrainLayer = GetComponent<LayerManager>().terrainLayer;
            
        }

        public void AddVegitation()
        {
            vegitationList.Add(new Vegitation());
        }

        public void RemoveVegitation()
        {
            if (vegitationList.Count > 0)
            {
                VegitationHelper.SetVegitationList(vegitationList);
                vegitationList = VegitationHelper.RemoveVegitation();
            }
        }

        public void PlantTree()
        {
            VegitationHelper.SetVegitationList(vegitationList);
            VegitationHelper.SetTreeSpacing(treeSpacing);
            VegitationHelper.SetMaxTrees(maxTrees);
            VegitationHelper.SetTerrainLayer(terrainLayer);
            VegitationHelper.SetTerrainPosition(this.transform.position);
            VegitationHelper.plantTree();
        }
    }
}
