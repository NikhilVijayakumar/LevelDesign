
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
    private GUITableState perlinParameterTable;
    private SerializedProperty perlinList;
    private SerializedProperty voronoiMinHeight;
    private SerializedProperty voronoiMaxHeight;
    private SerializedProperty voronoiFalloff;
    private SerializedProperty voronoiDropoff;
    private SerializedProperty voronoiPeakCount;
    private SerializedProperty midPointMinHeight;
    private SerializedProperty midPointMaxHeight;
    private SerializedProperty midPointRoughness;
    private SerializedProperty midPointHeightPower;
    private SerializedProperty smoothAmount;
    private SerializedProperty directoryPath ;
    private SerializedProperty filename ;


    private SerializedProperty multiplePerlinXscale;
    private SerializedProperty multiplePerlinYscale;
    private SerializedProperty multiplePerlinXoffset;
    private SerializedProperty multiplePerlinYoffset;
    private SerializedProperty multiplePerlinOctave;
    private SerializedProperty multiplePerlinPersistance;
    private SerializedProperty multiplePerlinHeightScale;
    private SerializedProperty multiplePerlinPostion;

    //show hide properties
    private bool showRandom = false;
    private bool showHeightMapImage = false;
    private bool showPerlinMap = false;
    private bool showMultiplePerlinMap = false;
    private bool showVoronoiMap = false;
    private bool showMidPointMap = false;
    private bool showSmooth = false;
    private bool showSplatmap = false;
    private bool showSave = false;




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
        multiplePerlinXscale = serializedObject.FindProperty("multiplePerlinXscale");
        multiplePerlinYscale = serializedObject.FindProperty("multiplePerlinYscale");
        multiplePerlinXoffset = serializedObject.FindProperty("multiplePerlinXoffset");
        multiplePerlinYoffset = serializedObject.FindProperty("multiplePerlinYoffset");
        multiplePerlinOctave = serializedObject.FindProperty("multiplePerlinOctave");
        multiplePerlinPersistance = serializedObject.FindProperty("multiplePerlinPersistance");
        multiplePerlinHeightScale = serializedObject.FindProperty("multiplePerlinHeightScale");
        multiplePerlinPostion = serializedObject.FindProperty("multiplePerlinPostion");
        perlinParameterTable = new GUITableState("perlinParameterTable");
        perlinList = serializedObject.FindProperty("perlinList");
        voronoiMinHeight = serializedObject.FindProperty("voronoiMinHeight");
        voronoiMaxHeight = serializedObject.FindProperty("voronoiMaxHeight");
        voronoiFalloff = serializedObject.FindProperty("voronoiFalloff");
        voronoiDropoff = serializedObject.FindProperty("voronoiDropoff");
        voronoiPeakCount = serializedObject.FindProperty("voronoiPeakCount");
        midPointMinHeight = serializedObject.FindProperty("midPointMinHeight");
        midPointMaxHeight = serializedObject.FindProperty("midPointMaxHeight");
        midPointRoughness = serializedObject.FindProperty("midPointRoughness");
        midPointHeightPower = serializedObject.FindProperty("midPointHeightPower");
        smoothAmount = serializedObject.FindProperty("smoothAmount");
        filename = serializedObject.FindProperty("filename");
        directoryPath = serializedObject.FindProperty("directoryPath");

      

}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        LoadHeightMap();
        SaveTerrainHeight();
        GenerateRandomTerrain();      
        PerlinTerrain();
        MultiplePerlinTerrain();
        VoronoiTerrain();
        MidPointTerrain();      
        SmoothTerrain();
        ResetTerrain();   
        serializedObject.ApplyModifiedProperties();
    }


    private void SaveTerrainHeight()
    {
        showSave = EditorGUILayout.Foldout(showSave, "Save Terrain");
        if (showSave)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.PropertyField(filename);
            EditorGUILayout.PropertyField(directoryPath);
            if (GUILayout.Button("Save"))
            {
                terrain.SaveTerrainHeight();
            }
        }
    }


    private void LoadHeightMap()
    {
        showHeightMapImage = EditorGUILayout.Foldout(showHeightMapImage, "Load Terrain");
        if (showHeightMapImage)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.PropertyField(heighMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            if (GUILayout.Button("Load"))
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

    private void MidPointTerrain()
    {
        showMidPointMap = EditorGUILayout.Foldout(showMidPointMap, "MidPoint map");
        if (showMidPointMap)
        {
            EditorGUILayout.Slider(midPointMinHeight, 0, 1f, new GUIContent("MinHeight "));
            EditorGUILayout.Slider(midPointMaxHeight, 0, 1f, new GUIContent("MaxHeight"));
            EditorGUILayout.Slider(midPointRoughness, 0, 1.5f, new GUIContent("Roughness"));
            EditorGUILayout.Slider(midPointHeightPower, 1, 2f, new GUIContent("Height Dampner power"));

            if (GUILayout.Button("MidPoint Terrain"))
            {
                terrain.GetMidPointTerrain();
            }
        }
    }

    private void VoronoiTerrain()
    {

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


            int count = terrain.GetPerlinCount(); ;
          
            EditorGUILayout.Slider(multiplePerlinXscale, 0, 0.1f, new GUIContent("X scale"));
            EditorGUILayout.Slider(multiplePerlinYscale, 0, 0.1f, new GUIContent("Y scale"));
            EditorGUILayout.Slider(multiplePerlinPersistance, 0, 10f, new GUIContent("Persistance"));
            EditorGUILayout.Slider(multiplePerlinHeightScale, 0, 1f, new GUIContent("HeightScale"));
            EditorGUILayout.IntSlider(multiplePerlinXoffset, 0, 10000, new GUIContent("X Offset"));
            EditorGUILayout.IntSlider(multiplePerlinYoffset, 0, 10000, new GUIContent("Y offset"));
            EditorGUILayout.IntSlider(multiplePerlinOctave, 0, 10, new GUIContent("Octave"));

            if (count >= 1)
            {
                EditorGUILayout.IntSlider(multiplePerlinPostion, 0, count, new GUIContent("Postion"));
            }
            

            if (GUILayout.Button("Add To Table"))
            {
                terrain.AddPerlinToTable();
            }

            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            perlinParameterTable = GUITableLayout.DrawTable(perlinParameterTable, perlinList);
            GUILayout.Space(30);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddPerlin();
            }

            if (GUILayout.Button("-"))
            {
                terrain.RemovePerlin();
            }
             EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Multiple Perlin Terrain"))
            {
                terrain.GetMultiplePerlinTerrain();
            }
        }

    }


    private void SmoothTerrain()
    {
        
        showSmooth = EditorGUILayout.Foldout(showSmooth, "Smooth");
        if (showSmooth)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.IntSlider(smoothAmount, 1, 10, new GUIContent("Smooth Amount"));
            if (GUILayout.Button("Smooth"))
            {
                terrain.SmoothTerrain();
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

