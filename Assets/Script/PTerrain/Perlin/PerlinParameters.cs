
namespace LevelDesign.PTerrain.Perlin
{
    [System.Serializable]
    public class PerlinParameters 
    {
        public float perlinXscale = 0.01f;
        public float perlinYscale = 0.01f;
        public int perlinXoffset = 1;
        public int perlinYoffset = 1;
        public int perlinOctave = 8;
        public float perlinPersistance;
        public float perlinHeightScale = 0.01f;
        public bool remove = false;
    }
}


