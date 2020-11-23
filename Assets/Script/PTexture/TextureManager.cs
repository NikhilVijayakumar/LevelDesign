using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using LevelDesign.PTexture.TSplatmap;

namespace LevelDesign.PTexture
{

    [ExecuteInEditMode]
    public class TextureManager : MonoBehaviour
    {
        //helper
        private SplatmapHelper splatmapHelper;      

        //Terrain info
        public Terrain terrain;
        public TerrainData terrainData;

        //Splatmap       
        public List<SplatHeight> splatHeightList = new List<SplatHeight>()
        {
            new SplatHeight()
        };


        private void OnEnable()
        {
            Debug.Log("OnEnable TextureManager");
            initData();
            initHelper();           
        }

        private void initHelper()
        {            
            splatmapHelper = new SplatmapHelper();
        }

        private void initData()
        {

            terrain = GetComponent<Terrain>();
            terrainData = Terrain.activeTerrain.terrainData;
        }     
                   
        public void GetSplashHeightTexture()
        {           
            TerrainLayer[] terrainLayers = new TerrainLayer[splatHeightList.Count];
            int index = 0;
            foreach (SplatHeight splatHeight in splatHeightList)
            {
                terrainLayers[index] = splatmapHelper.GetTerrainLayer(splatHeight);
                string path = "Assets/New Terrain Layer" + index + ".terrainlayer";
                AssetDatabase.CreateAsset(terrainLayers[index], path);
                Selection.activeObject = this.gameObject;
                index++;               
            }
            terrainData.terrainLayers = terrainLayers;
           splatmapHelper.ApplyHeight(terrainData, splatHeightList);
        }


        public void addSplashHeight()
        {
            splatHeightList.Add(new SplatHeight());
        }

        public void removeSplashHeight()
        {

            if (splatHeightList.Count > 0)
            {
                splatHeightList = splatmapHelper.removeSplashHeight(splatHeightList);
            }
        }

    }
}
