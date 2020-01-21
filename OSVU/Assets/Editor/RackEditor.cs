using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LandingRack))]
public class RackEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LandingRack rack = (LandingRack)target;
        
        if (GUILayout.Button("Open"))
        {
            rack.openRack();
        }

        if (GUILayout.Button("Close"))
        {
            rack.closeRack();
        }
    }
}
