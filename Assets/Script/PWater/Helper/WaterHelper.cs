using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.PWater.Helper
{
    public class WaterHelper
    {

        private TerrainData terrainData;
        private GameObject water;     
       

        public WaterHelper(TerrainData terrainData)
        {
            this.terrainData = terrainData;
        }

        public void SetWater(GameObject water)
        {
            this.water = water;
        }

        public void AddWaterDetails()
        {
                 
        }
       
    }   

}
