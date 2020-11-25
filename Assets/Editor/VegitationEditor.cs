
using UnityEditor;
using UnityEngine;
using EditorGUITable;
using LevelDesign.PVegitation;


[CustomEditor(typeof(VegitationManager))]
[CanEditMultipleObjects]
public class VegitationEditor : Editor
{
    private VegitationManager vegitation;


    //Serilizable properties
    private GUITableState vegitationTable;
    private SerializedProperty vegitationList;

    private SerializedProperty maxTrees;
    private SerializedProperty treeSpacing;


    //show hide properties
    private bool showVegitation = false;




    private void OnEnable()
    {
        vegitation = (VegitationManager)target;
        initSerilizeValue();
        
        
    }

    private void initSerilizeValue()
    {
        vegitationTable = new GUITableState("vegitationTable");
        vegitationList = serializedObject.FindProperty("vegitationList");
        maxTrees = serializedObject.FindProperty("maxTrees");
        treeSpacing = serializedObject.FindProperty("treeSpacing");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();  
        SplatHeightTexture();       
        serializedObject.ApplyModifiedProperties();
    }

    private void SplatHeightTexture()
    {
        showVegitation = EditorGUILayout.Foldout(showVegitation, "Vegitation");
        if (showVegitation)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUILayout.IntSlider(maxTrees, 0, 10000, new GUIContent("max Trees"));
            EditorGUILayout.IntSlider(treeSpacing, 16, 75, new GUIContent("tree Spacing"));

            vegitationTable = GUITableLayout.DrawTable(vegitationTable, vegitationList);
            GUILayout.Space(30);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                vegitation.AddVegitation();
            }

            if (GUILayout.Button("-"))
            {
                vegitation.RemoveVegitation();
            }
            EditorGUILayout.EndHorizontal();
          
            if (GUILayout.Button("Plant"))
            {
                vegitation.PlantTree();
            }
        }
    }

    
}

