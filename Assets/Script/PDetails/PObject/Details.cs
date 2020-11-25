
using UnityEngine;

namespace LevelDesign.PDetails.PObject
{
    [System.Serializable]
    public class Details 
    {
        public GameObject prototype;
        public Texture2D prototypeTexture;
        public float minHeight = 0.1f;
        public float maxHeight = 0.2f;
        public float minSteepness = 0f;
        public float maxSteepness = 90f;
        public Color dryColor = Color.white;
        public Color healthyColor = Color.white;
        public Vector2 heightRange = new Vector2(1, 1);
        public Vector2 widthRange = new Vector2(1, 1);
        public float noiseSpread = 0.5f;
        public float overlap = 0.01f;
        public float feather = 0.05f;
        public float density = 0.5f;
        public bool remove = false;
    }
}


