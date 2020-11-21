using LevelDesign.PTerrain.Tag.Runtime;
using LevelDesign.PTerrain.Tag.Edit;
using UnityEditor;
using UnityEngine;

namespace LevelDesign.PTerrain.Tag
{
    public class TagProvider
    {        
       public BaseTagManager GetTagManager(SerializedObject manager, TagModel tagModel,Mode runtime)
        {
            if (runtime.Equals(Mode.Runtime))
            {               
                return  new TagManager(manager, tagModel);
            }          

            return new TagEditManager(manager, tagModel);
        }

    }
}
