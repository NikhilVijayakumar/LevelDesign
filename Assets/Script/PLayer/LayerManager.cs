
using System;
using UnityEditor;
using UnityEngine;


namespace LevelDesign.PLayer
{

    [ExecuteInEditMode]
    public class LayerManager : MonoBehaviour
    {
        public const string TERRAIN = "Terrain";
        public const string CLOUD = "Cloud";
        public const string SHORE = "Shore";
        [SerializeField]
        public int terrainLayer = -1;
        private void Awake()
        {
            InitLayers();          
        }

        void InitLayers()
        {
            SerializedObject manager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layerProp = manager.FindProperty("layers");
            terrainLayer = addLayer(layerProp, TERRAIN);
            manager.ApplyModifiedProperties();
            AddTerrainLayer(gameObject);
        }

        private int addLayer(SerializedProperty layerProp,string newLayer)
        {
            bool found = false;
            for (int i = 0; i < layerProp.arraySize; i++)
            {
                if (layerProp.GetArrayElementAtIndex(i).stringValue.Equals(newLayer))
                {
                    found = true;
                    return i;
                }
            }
            for (int j = 8; j < layerProp.arraySize; j++)
            {
                SerializedProperty layer = layerProp.GetArrayElementAtIndex(j);
                if(layer.stringValue == "")
                {
                    layer.stringValue = newLayer;
                    return j;
                }
            }
            return -1;
        }

        public void AddTerrainLayer(GameObject gameObject)
        {
            gameObject.layer = terrainLayer;
        }
    }
}
