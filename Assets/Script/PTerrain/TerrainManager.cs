using UnityEditor;
using UnityEngine;
using System;
using LevelDesign.PTerrain.Tag;
using LevelDesign.PTerrain.TRandom;
using LevelDesign.PTerrain.Common;
using System.Collections.Generic;
using LevelDesign.PTerrain.Perlin;
using LevelDesign.PTerrain.Voronoi;

namespace LevelDesign.PTerrain
{

    [ExecuteInEditMode]
    public class TerrainManager : MonoBehaviour
    {
        //Helper class and model
        private BaseTagManager tagManager;
        private TagProvider tagProvider;
        private RandomTerrainHelper randomTerrainHelper;
        private CommonTerrainHelper commonTerrainHelper;
        private PerlinHelper perlinHelper;
        private VoronoiHelper voronoiHelper;
        private Mode mode;

        //Terrain info
        public Terrain terrain;
        public TerrainData terrainData;

        //Data and properties in inspector
        public TerrainModel data;

        //Random height
        public Vector2 randomHeightRange;
        public Texture2D heighMapImage;
        public Vector3 heightMapScale = new Vector3(1, 1, 1);

        //Perlin data
        public float perlinXscale = 0.01f;
        public float perlinYscale = 0.01f;
        public int perlinXoffset = 1;
        public int perlinYoffset = 1;
        public int perlinOctave = 8;
        public float perlinPersistance;
        public float perlinHeightScale = 0.01f;

        //Voronoi data
        public float voronoiMinHeight = 0.1f;
        public float voronoiMaxHeight = 1f;
        public float voronoiFalloff = 0.2f;
        public float voronoiDropoff = 0.6f;
        public int voronoiPeakCount = 8;


        //Multiple Perlin
        public List<PerlinParameters> perlinList = new List<PerlinParameters>()
        {
            new PerlinParameters()
        };

        private void OnEnable()
        {
            Debug.Log("OnEnable");
            initData();
            initHelper();           
        }

        private void initHelper()
        {
            randomTerrainHelper = new RandomTerrainHelper();
            commonTerrainHelper = new CommonTerrainHelper();
            perlinHelper = new PerlinHelper();
            voronoiHelper = new VoronoiHelper();
        }

        private void initData()
        {
            terrain = GetComponent<Terrain>();
            terrainData = Terrain.activeTerrain.terrainData;
        }

        private void Awake()
        {
            SerializedObject manager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);           
            isPlayMode();
            setTag(manager);           
        }

        private void setTag(SerializedObject manager)
        {
            if (data != null)
            {
                tagProvider = new TagProvider();
                tagManager = tagProvider.GetTagManager(manager, data.tagModel, mode);
                tagManager.InitTags();
                tagManager.addTerrainTag(gameObject);
            }
        }

        private void isPlayMode()
        {
            if (EditorApplication.isPlaying)
            {
                mode = Mode.Runtime;
            }
            else
            {
                mode = Mode.Edit;
            }
        }

        public void GetRandomTerrain()
        {            
            randomTerrainHelper.GenerateTerrain(terrainData, randomHeightRange);
        }

        public void ResetTerrain()
        {           
            commonTerrainHelper.ResetTerrain(terrainData);
        }

        public void LoadHeightMap()
        {
            if(heighMapImage != null)
            {
                randomTerrainHelper.LoadTerrain(terrainData,heighMapImage,heightMapScale);
            }           
        }

        public void GetPerlinTerrain()
        {
            perlinHelper.GetPerlinTerrain(terrainData,perlinXscale,perlinYscale,perlinXoffset,perlinYoffset,perlinOctave,perlinPersistance,perlinHeightScale);
        }


        public void GetMultiplePerlinTerrain()
        {
            perlinHelper.GetMultiplePerlinTerrain(terrainData, perlinList);
        }


        public void addPerlin()
        {
            perlinList.Add(new PerlinParameters());
        }

        public void removePerlin()
        {

         if(perlinList.Count > 0)
            {
                perlinList = perlinHelper.removePerlin(perlinList);
            }
        }

        public void GetVoronoiTerrain()
        {
            voronoiHelper.GetVoronoiTerrain(terrainData,voronoiMinHeight,voronoiMaxHeight,voronoiFalloff,voronoiDropoff,voronoiPeakCount);                
        }

    }
}
