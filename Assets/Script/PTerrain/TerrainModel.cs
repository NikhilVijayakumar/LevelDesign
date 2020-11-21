using LevelDesign.PTerrain.Tag;
using UnityEngine;

namespace LevelDesign.PTerrain
{
    [CreateAssetMenu(fileName = "TerrainData", menuName = "PTerrain/TerrainData")]
    public class TerrainModel : ScriptableObject
    {
        public TagModel tagModel;
    }
}



