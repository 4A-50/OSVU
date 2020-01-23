using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SceneSwitcher : MonoBehaviour
{
    public SteamVR_LoadLevel levelLoader;

    public void sceneSwitch(string sceneName)
    {
        levelLoader.levelName = sceneName;
        levelLoader.Trigger();
    }
}
