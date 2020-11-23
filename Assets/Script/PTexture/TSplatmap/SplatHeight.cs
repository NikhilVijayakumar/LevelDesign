
using UnityEngine;

namespace LevelDesign.PTexture.TSplatmap
{
    [System.Serializable]
    public class SplatHeight
    {
        public Texture2D texture;
        public float minHeight = 0.1f;
        public float maxHeight = 0.2f;
        public Vector2 tileOffset = new Vector2(0, 0);
        public Vector2 tileSize = new Vector2(50, 50);
        public bool remove = false;
    }
}
