using UnityEditor;
using UnityEngine;

namespace LevelDesign.PTerrain.Tag.Runtime
{
    public class TagManager : BaseTagManager
    {
    
        public TagManager(SerializedObject manager, TagModel tagModel)
        {
            this.manager = manager;
            data = tagModel;                     
        }

       override public void InitTags()
        {
            SerializedProperty tagArray = manager.FindProperty("tags");
            foreach(string tag in data.tagList ){
                AddTag(tagArray, tag);
            }          
          
            manager.ApplyModifiedProperties();

        }

        override protected void AddTag(SerializedProperty tagArray, string newTag)
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

        override public void addTerrainTag(GameObject gameObject,string tagname = TERRAIN)
        {
            gameObject.tag = tagname;
        }

        override public void addCloudTag(GameObject gameObject, string tagname = CLOUD)
        {
            gameObject.tag = tagname;
        }

        override public void addShoreTag(GameObject gameObject, string tagname = SHORE)
        {
            gameObject.tag = tagname;
        }
    }
}
