using UnityEngine;
using System.Collections.Generic;
using LevelDesign.PDetails.PObject;

namespace LevelDesign.PDetails
{

    [ExecuteInEditMode]
    public class DetailManager : MonoBehaviour
    {
       

        //Terrain info
        public Terrain terrain;
        public TerrainData terrainData;

        private DetailHelper detailHelper;



        //Details  
        public int maxDetails = 100;
        public int detailSpacing = 5;

        public List<Details> detailList = new List<Details>()
        {
            new Details()
        };


        private void OnEnable()
        {
            Debug.Log("OnEnable TextureManager");
            initData();
            initHelper();           
        }

        private void initHelper()
        {
            detailHelper = new DetailHelper(terrainData);
        }

        private void initData()
        {
            terrain = GetComponent<Terrain>();
            terrainData = Terrain.activeTerrain.terrainData;     
        }

        public void AddDetailList()
        {
            detailList.Add(new Details());
        }

        public void RemoveDetailList()
        {
            if (detailList.Count > 0)
            {
                detailHelper.SetDetailsList(detailList);
                detailList = detailHelper.RemoveDetails();
            }
        }

        public void AddDetails()
        {
            detailHelper.SetDetailsList(detailList);
            detailHelper.SetDetailSpacing(detailSpacing);
            detailHelper.AddDetails();
        }
    }
}
