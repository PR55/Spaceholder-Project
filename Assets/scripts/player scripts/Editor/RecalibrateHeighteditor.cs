using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(RecalibrateHeight))]
public class RecalibrateHeighteditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RecalibrateHeight a = (RecalibrateHeight)target;

        if(GUILayout.Button("Resize Player"))
        {
            a.editorResize();
        }
    }
}
