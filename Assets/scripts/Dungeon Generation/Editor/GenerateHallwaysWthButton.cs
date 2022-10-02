using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HallwayGeneration))]
public class GenerateHallwaysWthButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HallwayGeneration hallway = (HallwayGeneration)target;
        if(GUILayout.Button("Create Hallways"))
        {
            hallway.pointCheck(hallway.startPoint,hallway.endPoint);
        }
    }
}
