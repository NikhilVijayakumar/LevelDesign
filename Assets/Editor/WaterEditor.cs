
using UnityEditor;
using UnityEngine;
using EditorGUITable;
using LevelDesign.PWater;


[CustomEditor(typeof(WaterManager))]
[CanEditMultipleObjects]
public class WaterEditor : Editor
{
    private WaterManager waterManager;


    //Serilizable properties 
    private SerializedProperty waterHeight;
    private SerializedProperty waterGO;
    private SerializedProperty shoreLineMaterial;


    //show hide properties
    private bool showDetails = false;




    private void OnEnable()
    {
        waterManager = (WaterManager)target;
        initSerilizeValue();
        
        
    }

    private void initSerilizeValue()
    {
        waterHeight = serializedObject.FindProperty("waterHeight");
        waterGO = serializedObject.FindProperty("waterGO");
        shoreLineMaterial = serializedObject.FindProperty("shoreLineMaterial");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();  
        Details();       
        serializedObject.ApplyModifiedProperties();
    }

    private void Details()
    {
        showDetails = EditorGUILayout.Foldout(showDetails, "Water Details");
        if (showDetails)
        {
           EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
           EditorGUILayout.Slider(waterHeight, 0f, 1f, new GUIContent("Water Height"));
           EditorGUILayout.PropertyField(waterGO);
            
            if (GUILayout.Button("Add Water"))
            {
                waterManager.AddWaterDetails();
            }

            EditorGUILayout.PropertyField(shoreLineMaterial);
            if (GUILayout.Button("Add Wave"))
            {
                waterManager.DrawShoreLine();
            }

        }
    }

    
}

