#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class EventEditorGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent myGameEvent = (GameEvent)target;

        if (GUILayout.Button("Raise()"))
        {
            myGameEvent.Raise();
        }
    }
}
#endif
