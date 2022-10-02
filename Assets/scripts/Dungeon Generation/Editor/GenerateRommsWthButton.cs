using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generaterooms))]
public class GenerateRommsWthButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Generaterooms rooms = (Generaterooms)target;
        if(GUILayout.Button("Create Rooms"))
        {
            rooms.GenerateRooms();
        }
    }
}
