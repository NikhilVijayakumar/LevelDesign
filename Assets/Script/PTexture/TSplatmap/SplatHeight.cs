
using UnityEngine;

namespace LevelDesign.PTexture.TSplatmap
{
    [System.Serializable]
    public class SplatHeight
    {
        public Texture2D texture;
        public float minHeight = 0.1f;
        public float maxHeight = 0.2f;
        public float minSteepness= 0f;
        public float maxSteepness = 90f;
        public float offset = 0.001f;
        public float noiseX = 0.01f;
        public float noiseY = 0.01f;
        public float noiseScaler = 0.5f;
        public Vector2 tileOffset = new Vector2(0, 0);
        public Vector2 tileSize = new Vector2(50, 50);
        public bool remove = false;
    }
}
