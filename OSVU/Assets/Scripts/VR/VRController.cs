using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 0.1f;

    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_Speed = 0.0f;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }
    
    private void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    /// <summary>
    /// Handles The Head Rotation.
    /// Starts By Saving The Current Pos And Rot,
    /// Then Rotates The GameObject,
    /// Then Resets The Camera Rig To The Previous Position.
    /// </summary>
    private void HandleHead()
    {
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        transform.eulerAngles = new Vector3(0, m_Head.rotation.eulerAngles.y, 0);

        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;
    }

    /// <summary>
    /// Calculates The Movement Amount And Them Moves The Character Controller.
    /// Starts By Working Out Where The Headset Is Facing,
    /// Then Checks If The Buttons Been Pressed,
    /// If It Has It Works Out The Move And Moves It.
    /// </summary>
    private void CalculateMovement()
    {
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        if (m_MovePress.GetLastStateUp(SteamVR_Input_Sources.Any))
            m_Speed = 0;

        if (m_MovePress.state)
        {
            m_Speed += m_MoveValue.axis.y * m_Sensitivity;
            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);

            movement += orientation * (m_Speed * Vector3.forward) * Time.deltaTime;
        }

        m_CharacterController.Move(movement);
    }

    /// <summary>
    /// Works Out The Distance From The Headset To The Ground, Then Sets The CC's Hieght To It.
    /// Halfs The Height Then Recenters It,
    /// Then It Aligns The CC To The Correct Position.
    /// </summary>
    private void HandleHeight()
    {
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        m_CharacterController.center = newCenter;
    }
}
