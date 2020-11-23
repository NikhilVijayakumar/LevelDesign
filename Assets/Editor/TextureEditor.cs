
using UnityEditor;
using UnityEngine;
using EditorGUITable;
using LevelDesign.PTexture;


[CustomEditor(typeof(TextureManager))]
[CanEditMultipleObjects]
public class TextureEditor : Editor
{
    private TextureManager texture;


    //Serilizable properties
    private GUITableState splatmapHeightTable;
    private SerializedProperty splatHeightList;


    //show hide properties
    private bool showSplatmap = false;




    private void OnEnable()
    {
        texture = (TextureManager)target;
        initSerilizeValue();
        
        
    }

    private void initSerilizeValue()
    {       
        splatmapHeightTable = new GUITableState("splatmapHeightTable");
        splatHeightList = serializedObject.FindProperty("splatHeightList");
}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();  
        SplatHeightTexture();       
        serializedObject.ApplyModifiedProperties();
    }

    private void SplatHeightTexture()
    {
        showSplatmap = EditorGUILayout.Foldout(showSplatmap, "Splatmap Texture");
        if (showSplatmap)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            splatmapHeightTable = GUITableLayout.DrawTable(splatmapHeightTable, splatHeightList);
            GUILayout.Space(30);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                texture.addSplashHeight();
            }

            if (GUILayout.Button("-"))
            {
                texture.removeSplashHeight();
            }
            EditorGUILayout.EndHorizontal();
          
            if (GUILayout.Button("Splash Height"))
            {
                texture.GetSplashHeightTexture();
            }
        }
    }

    
}

