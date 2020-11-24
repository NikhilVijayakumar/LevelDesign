
using UnityEngine;

namespace LevelDesign.PVegitation.PTree
{
    [System.Serializable]
    public class Vegitation 
    {
        public GameObject mesh;
        public float minHeight = 0.1f;
        public float maxHeight = 0.2f;
        public float minSteepness = 0f;
        public float maxSteepness = 90f;
        public bool remove = false;
    }
}


