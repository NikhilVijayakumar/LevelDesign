
using UnityEditor;
using UnityEngine;
using EditorGUITable;
using LevelDesign.PDetails;


[CustomEditor(typeof(DetailManager))]
[CanEditMultipleObjects]
public class DetailsEditor : Editor
{
    private DetailManager detailManager;


    //Serilizable properties
    private GUITableState detailTable;
    private SerializedProperty detailList;
    private SerializedProperty maxDetails;
    private SerializedProperty detailSpacing;


    //show hide properties
    private bool showDetails = false;




    private void OnEnable()
    {
        detailManager = (DetailManager)target;
        initSerilizeValue();
        
        
    }

    private void initSerilizeValue()
    {
        detailTable = new GUITableState("detailTable");
        detailList = serializedObject.FindProperty("detailList");
        maxDetails = serializedObject.FindProperty("maxDetails");
        detailSpacing = serializedObject.FindProperty("detailSpacing");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();  
        Details();       
        serializedObject.ApplyModifiedProperties();
    }

    private void Details()
    {
        showDetails = EditorGUILayout.Foldout(showDetails, "Details");
        if (showDetails)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.IntSlider(maxDetails, 0, 10000, new GUIContent("max Details"));
            EditorGUILayout.IntSlider(detailSpacing, 16, 75, new GUIContent("Details Spacing"));

            detailManager.GetComponent<Terrain>().detailObjectDistance = maxDetails.intValue;

            detailTable = GUITableLayout.DrawTable(detailTable, detailList);
            GUILayout.Space(30);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                detailManager.AddDetailList();
            }

            if (GUILayout.Button("-"))
            {
                detailManager.RemoveDetailList();
            }
            EditorGUILayout.EndHorizontal();
          
            if (GUILayout.Button("Plant"))
            {
                detailManager.AddDetails();
            }
        }
    }

    
}

