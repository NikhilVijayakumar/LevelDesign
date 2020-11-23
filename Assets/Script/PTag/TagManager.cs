
using UnityEditor;
using UnityEngine;


namespace LevelDesign.PTag
{

    [ExecuteInEditMode]
    public class TagManager : MonoBehaviour
    {
        public const string TERRAIN = "Terrain";
        public const string CLOUD = "Cloud";
        public const string SHORE = "Shore";

        private void Awake()
        {
            InitTags();
            addTerrainTag(gameObject);
        }

        void InitTags()
        {
            SerializedObject manager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty tagArray = manager.FindProperty("tags");
            AddTag(tagArray, TERRAIN);
            AddTag(tagArray, CLOUD);
            AddTag(tagArray, SHORE);
            manager.ApplyModifiedProperties();      
        }

        void AddTag(SerializedProperty tagArray, string newTag)
        {
            bool found = false;
            for (int i = 0; i < tagArray.arraySize; i++)
            {
                if (tagArray.GetArrayElementAtIndex(i).stringValue.Equals(newTag))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                tagArray.InsertArrayElementAtIndex(0);
                SerializedProperty data = tagArray.GetArrayElementAtIndex(0);
                data.stringValue = newTag;
            }
        }

        public void addTerrainTag(GameObject gameObject, string tagname = TERRAIN)
        {
            gameObject.tag = tagname;
        }

        public void addCloudTag(GameObject gameObject, string tagname = CLOUD)
        {
            gameObject.tag = tagname;
        }

        public void addShoreTag(GameObject gameObject, string tagname = SHORE)
        {
            gameObject.tag = tagname;
        }
    }
}
