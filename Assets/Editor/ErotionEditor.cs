
using UnityEditor;
using UnityEngine;
using EditorGUITable;
using LevelDesign.PErotion;


[CustomEditor(typeof(ErotionManager))]
[CanEditMultipleObjects]
public class ErotionEditor: Editor
{
    private ErotionManager erotionManager;


    //Serilizable properties    
    private SerializedProperty erosionType ;
    public SerializedProperty erosionStrength ;
    public SerializedProperty erosionAmount;
    public SerializedProperty springPerRiver;
    public SerializedProperty solubility;
    public SerializedProperty droplets;
    public SerializedProperty erosionSmoothAmount;
    private SerializedProperty waterHeight;
    private SerializedProperty windDirection;


    //show hide properties
    private bool showErotion = false;

    private void OnEnable()
    {
        erotionManager = (ErotionManager)target;
        initSerilizeValue();   
    }

    private void initSerilizeValue()
    {
        waterHeight = serializedObject.FindProperty("waterHeight");
        erosionType = serializedObject.FindProperty("erosionType");
        erosionStrength = serializedObject.FindProperty("erosionStrength");
        erosionAmount = serializedObject.FindProperty("erosionAmount");
        springPerRiver = serializedObject.FindProperty("springPerRiver");
        solubility = serializedObject.FindProperty("solubility");
        droplets = serializedObject.FindProperty("droplets");
        erosionSmoothAmount = serializedObject.FindProperty("erosionSmoothAmount");
        windDirection = serializedObject.FindProperty("windDirection");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();  
        Details();       
        serializedObject.ApplyModifiedProperties();
    }

    private void Details()
    {
        showErotion = EditorGUILayout.Foldout(showErotion, "Erotion");
        if (showErotion)
        {
           EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.PropertyField(erosionType);
            EditorGUILayout.Slider(erosionStrength, 0.1f, 1f, new GUIContent("Erosion Strength"));
            EditorGUILayout.Slider(erosionAmount, 0.001f, 1f, new GUIContent("Erosion Amount"));
            EditorGUILayout.Slider(waterHeight, 0f, 1f, new GUIContent("Water Height"));
            EditorGUILayout.IntSlider(springPerRiver, 0, 20, new GUIContent("Spring Per River"));
            EditorGUILayout.Slider(windDirection, 0.01f, 60f, new GUIContent("Wind Direction"));
            EditorGUILayout.Slider(solubility, 0.001f, 1f, new GUIContent("Solubility"));
            EditorGUILayout.IntSlider(droplets, 0, 500, new GUIContent("Droplets"));
            EditorGUILayout.IntSlider(erosionSmoothAmount, 0, 10, new GUIContent("Erosion Smooth Amount"));

            if (GUILayout.Button("Erode"))
            {
                erotionManager.Erode();
            }          
        }
    }

    
}

