
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
        public float minScale = 0.5f;
        public float maxScale = 1f;
        public Color color1 = Color.white;
        public Color color2 = Color.white;
        public Color lightColor = Color.white;
        public float minRotation = 0f;
        public float maxRotation = 360f;
        public float density = 0.5f;
        public bool remove = false;
    }
}


