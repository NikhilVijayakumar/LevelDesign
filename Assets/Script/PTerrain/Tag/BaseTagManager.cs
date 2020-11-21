using UnityEditor;
using UnityEngine;

namespace LevelDesign.PTerrain.Tag
{
    public abstract class BaseTagManager
    {
        protected const string TERRAIN = "Terrain";
        protected const string CLOUD = "Cloud";
        protected const string SHORE = "Shore";       
        protected SerializedObject manager;
        protected TagModel data;

        public abstract void InitTags();


        protected abstract void AddTag(SerializedProperty tagArray, string newTag);


        public abstract void addTerrainTag(GameObject gameObject, string tagname = TERRAIN);


        public abstract void addCloudTag(GameObject gameObject, string tagname = CLOUD);


        public abstract void addShoreTag(GameObject gameObject, string tagname = SHORE);
        


    }
}
