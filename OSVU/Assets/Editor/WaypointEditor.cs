using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpaceLanes))]
public class WaypointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpaceLanes lanes = (SpaceLanes)target;

        EditorGUILayout.MinMaxSlider(ref lanes.min, ref lanes.max, lanes.minLimit, lanes.maxLimit);

        if (GUILayout.Button("Create Full Path"))
        {
            lanes.CreateRoute();
        }
    }
}
