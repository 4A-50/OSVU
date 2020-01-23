using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpaceLanes))]
public class WaypointEditor : Editor
{
    public string[] options = new string[] { "Straight Line", "Constriant Path"};
    public int index = 0;

    SerializedProperty laneMin;
    SerializedProperty laneMax;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpaceLanes lanes = (SpaceLanes)target;
        laneMin = serializedObject.FindProperty("min");
        laneMax = serializedObject.FindProperty("max");

        index = EditorGUILayout.Popup(index, options);

        switch (index)
        {
            case 0:
                if (GUILayout.Button("Create line"))
                {
                    lanes.CreateStraight();
                }
                break;
            case 1:
                EditorGUILayout.MinMaxSlider(ref lanes.min, ref lanes.max, lanes.minLimit, lanes.maxLimit);
                EditorGUILayout.PropertyField(laneMin);
                EditorGUILayout.PropertyField(laneMax);

                if (GUILayout.Button("Create Full Path"))
                {
                    lanes.CreateRoute();
                }
                break;
        }
    }
}
