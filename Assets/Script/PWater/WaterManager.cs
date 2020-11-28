using UnityEngine;
using System.Collections.Generic;
using LevelDesign.PTerrain.Common;

namespace LevelDesign.PWater
{

    [ExecuteInEditMode]
    public class WaterManager : MonoBehaviour
    {
        //Terrain info
        public Terrain terrain;
        public TerrainData terrainData;
        private const string WATER = "water";
        private CommonTerrainHelper helper;

        private const string SHORE = "Shore";
        private const string SHORELINE = "ShoreLine";

        //WaterDetails  
        public float waterHeight = 0f;
        public GameObject waterGO;
        public Material shoreLineMaterial;

        private void OnEnable()
        {
            Debug.Log("OnEnable TextureManager");
            initData();          
        }

        private void initData()
        {
            terrain = GetComponent<Terrain>();
            terrainData = Terrain.activeTerrain.terrainData;
            helper = new CommonTerrainHelper();
        }

        public void AddWaterDetails()
        {
            GameObject water = GameObject.Find(WATER);
            if (!water)
            {
                water = Instantiate(waterGO, transform.position, transform.rotation);
                water.name = WATER;
            }
            water.transform.position = new Vector3(terrainData.size.x,
                                        waterHeight * terrainData.size.y,
                                        terrainData.size.z);
            water.transform.localScale = new Vector3(terrainData.size.x, 1, terrainData.size.z);
        }
        public void DrawShoreLine()
        {
            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);          
           
            for (int y = 0; y < terrainData.heightmapResolution;y++)
            {
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    Vector2 location = new Vector2(x, y);
                    List<Vector2> neighbours = helper.GenerateNeighbours(location,
                                                            terrainData.heightmapResolution,
                                                            terrainData.heightmapResolution);
                    foreach(Vector2 n in neighbours)
                    {
                        if(heightMap[x,y] < waterHeight
                            && heightMap[(int)n.x, (int)n.y] > waterHeight)
                        {
                           
                                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
                                go.transform.localScale *= 20.1f;
                                go.transform.position = transform.position +
                                    new Vector3(y / (float)terrainData.heightmapResolution * terrainData.size.z,
                                    waterHeight * terrainData.size.y,
                                    x / (float)terrainData.heightmapResolution * terrainData.size.x);
                            go.transform.LookAt(new Vector3(n.y / (float)terrainData.heightmapResolution * terrainData.size.z,
                                    waterHeight * terrainData.size.y,
                                    n.x / (float)terrainData.heightmapResolution * terrainData.size.x));
                            go.transform.Rotate(90, 0, 0);
                            go.tag = SHORE;
                           // go.transform.parent = quad.transform;                           
                        }
                    }
                }
            }
            combineShore();           
        }

        private void combineShore()
        {
            GameObject[] shoreQuads = GameObject.FindGameObjectsWithTag(SHORE);
            MeshFilter[] meshFilters = new MeshFilter[shoreQuads.Length];
             for (int m = 0; m < shoreQuads.Length; m++)
            {
                meshFilters[m] = shoreQuads[m].GetComponent<MeshFilter>();
            }
            CombineInstance[] instances = new CombineInstance[meshFilters.Length];
            for (int i = 0; i < meshFilters.Length; i++)
            {
                instances[i].mesh = meshFilters[i].sharedMesh;
                instances[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }

            GameObject currentShoreLine = GameObject.Find(SHORELINE);
            if (currentShoreLine)
            {
                DestroyImmediate(currentShoreLine);
            }
            GameObject shoreLine = new GameObject();
            shoreLine.name = SHORELINE;
            shoreLine.AddComponent<WaveAnimation>();
            shoreLine.transform.position = transform.position;
            shoreLine.transform.rotation = transform.rotation;
            MeshFilter meshFilter = shoreLine.AddComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            meshFilter.sharedMesh.CombineMeshes(instances);
            MeshRenderer renderer = shoreLine.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = shoreLineMaterial;

            for (int i = 0; i < shoreQuads.Length; i++)
            {
                DestroyImmediate(shoreQuads[i]);
            }
        }

      
    }
}
