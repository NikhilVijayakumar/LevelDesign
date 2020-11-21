
using UnityEditor;
using UnityEngine;
using EditorGUITable;
using LevelDesign.PTerrain;
using System;

[CustomEditor(typeof(TerrainManager))]
[CanEditMultipleObjects]
public class TerrainEditor : Editor
{
    private TerrainManager terrain;


    //Serilizable properties
    private SerializedProperty randomHeightRange;
    private SerializedProperty heighMapImage;
    private SerializedProperty heightMapScale;
    private SerializedProperty perlinXscale;
    private SerializedProperty perlinYscale;
    private SerializedProperty perlinXoffset;
    private SerializedProperty perlinYoffset;
    private SerializedProperty perlinOctave ;
    private SerializedProperty perlinPersistance;
    private SerializedProperty perlinHeightScale;
    private SerializedProperty voronoiMinHeight;
    private SerializedProperty voronoiMaxHeight;
    private SerializedProperty voronoiFalloff;
    private SerializedProperty voronoiDropoff;
    private SerializedProperty voronoiPeakCount;

    private GUITableState perlinParameterTable;
    private SerializedProperty perlinList;


    //show hide properties
    private bool showRandom = false;
    private bool showHeightMapImage = false;
    private bool showPerlinMap = false;
    private bool showMultiplePerlinMap = false;
    private bool showVoronoiMap = false;




    private void OnEnable()
    {
        terrain = (TerrainManager)target;
        initSerilizeValue();
        
        
    }

    private void initSerilizeValue()
    {
        randomHeightRange = serializedObject.FindProperty("randomHeightRange");
        heighMapImage = serializedObject.FindProperty("heighMapImage");
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        perlinXscale = serializedObject.FindProperty("perlinXscale");
        perlinYscale = serializedObject.FindProperty("perlinYscale");
        perlinXoffset = serializedObject.FindProperty("perlinXoffset");
        perlinYoffset = serializedObject.FindProperty("perlinYoffset");
        perlinOctave = serializedObject.FindProperty("perlinOctave");
        perlinPersistance = serializedObject.FindProperty("perlinPersistance");
        perlinHeightScale = serializedObject.FindProperty("perlinHeightScale");
        perlinParameterTable = new GUITableState("perlinParameterTable");
        perlinList = serializedObject.FindProperty("perlinList");
        voronoiMinHeight = serializedObject.FindProperty("voronoiMinHeight");
        voronoiMaxHeight = serializedObject.FindProperty("voronoiMaxHeight");
        voronoiFalloff = serializedObject.FindProperty("voronoiFalloff");
        voronoiDropoff = serializedObject.FindProperty("voronoiDropoff");
        voronoiPeakCount = serializedObject.FindProperty("voronoiPeakCount");
}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();       
        GenerateRandomTerrain();
        LoadHeightMap();
        PerlinTerrain();
        MultiplePerlinTerrain();
        VoronoiTerrain();
        ResetTerrain();
       
        serializedObject.ApplyModifiedProperties();
    }

    private void LoadHeightMap()
    {
        showHeightMapImage = EditorGUILayout.Foldout(showHeightMapImage, "Height map from image");
        if (showHeightMapImage)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.PropertyField(heighMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            if (GUILayout.Button("Load terrain"))
            {
                terrain.LoadHeightMap();
            }
        }
    }

    private void GenerateRandomTerrain()
    {
        showRandom = EditorGUILayout.Foldout(showRandom, "Random");
        if (showRandom)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.PropertyField(randomHeightRange);
            if(GUILayout.Button("Random Heights")){
                terrain.GetRandomTerrain();
            }
        }
    }

    private void VoronoiTerrain()
    {
/*         private SerializedProperty voronoiMinHeight;
    private SerializedProperty voronoiMaxHeight;
    private SerializedProperty voronoiFalloff;
    private SerializedProperty voronoiDropoff;
    private SerializedProperty voronoiPeakCount;*/

    showVoronoiMap = EditorGUILayout.Foldout(showVoronoiMap, "Voronoi Terrain");
        if (showVoronoiMap)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Slider(voronoiMinHeight, 0, 1f, new GUIContent("MinHeight "));
            EditorGUILayout.Slider(voronoiMaxHeight, 0, 1f, new GUIContent("MaxHeight"));
            EditorGUILayout.Slider(voronoiFalloff, 0, 10f, new GUIContent("Falloff"));
            EditorGUILayout.Slider(voronoiDropoff, 0, 10f, new GUIContent("Dropoff"));
            EditorGUILayout.IntSlider(voronoiPeakCount, 1, 10, new GUIContent("Peak Count"));     
            if (GUILayout.Button("Voronoi Terrain"))
            {
                terrain.GetVoronoiTerrain();
            }
        }

    }


    private void PerlinTerrain()
    {
        showPerlinMap = EditorGUILayout.Foldout(showPerlinMap, "Single Perlin Terrain");
        if (showPerlinMap)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Slider(perlinXscale,0,0.1f, new GUIContent("X scale"));
            EditorGUILayout.Slider(perlinYscale, 0, 0.1f, new GUIContent("Y scale"));
            EditorGUILayout.Slider(perlinPersistance, 0, 10f, new GUIContent("Persistance"));
            EditorGUILayout.Slider(perlinHeightScale, 0, 1f, new GUIContent("HeightScale"));
            EditorGUILayout.IntSlider(perlinXoffset, 0, 10000, new GUIContent("X Offset"));
            EditorGUILayout.IntSlider(perlinYoffset, 0, 10000, new GUIContent("Y offset"));
            EditorGUILayout.IntSlider(perlinOctave, 0, 10, new GUIContent("Octave"));


            if (GUILayout.Button("Perlin Terrain"))
            {
                terrain.GetPerlinTerrain();
            }
        }
     
    }


    private void MultiplePerlinTerrain()
    {
        showMultiplePerlinMap = EditorGUILayout.Foldout(showMultiplePerlinMap, "Multiple Perlin Terrain");
        if (showMultiplePerlinMap)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            perlinParameterTable = GUITableLayout.DrawTable(perlinParameterTable, perlinList);
            GUILayout.Space(30);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.addPerlin();
            }

            if (GUILayout.Button("-"))
            {
                terrain.removePerlin();
            }
             EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Multiple Perlin Terrain"))
            {
                terrain.GetMultiplePerlinTerrain();
            }
        }

    }


    private void ResetTerrain()
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Reset"))
        {
            terrain.ResetTerrain();
        }
    }
}
