using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Interactable))]
public class Camera : MonoBehaviour
{
    Interactable_OLD interactable;

    [SerializeField]
    UnityEngine.Camera cam;

    public SteamVR_Action_Boolean takePicture = null;

    void Start()
    {
        interactable = GetComponent<Interactable_OLD>();
    }

    void Update()
    {
        if (interactable.activeHand != null)
        {
            if (cam.enabled == false)
                cam.enabled = true;

            if (takePicture.GetStateDown(interactable.activeHand.pose.inputSource))
            {
                ScreenCapture.CaptureScreenshot(System.DateTime.Now.ToString("__yyyy-MM-dd") + ".png");
            }
        }
        else
        {
            if (cam.enabled == true)
                cam.enabled = false;
        }
    }
}
