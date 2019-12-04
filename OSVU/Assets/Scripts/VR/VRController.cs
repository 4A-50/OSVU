using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 0.1f;
    public float m_Gravity = 200f;
    public float m_ClimbSensitivity = 45f;

    private Hand m_CurrentClimbHand = null;

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
        Vector3 movement = Vector3.zero;

        if (!m_CurrentClimbHand)
        {
            float rotation = Mathf.Atan2(m_MoveValue.axis.x, m_MoveValue.axis.y);
            rotation *= Mathf.Rad2Deg;
            Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y + rotation, 0);
            Quaternion orientation = Quaternion.Euler(orientationEuler);

            if (m_MoveValue.axis.magnitude == 0)
                m_Speed = 0;


            m_Speed += m_MoveValue.axis.magnitude * m_Sensitivity;
            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);

            movement += orientation * (m_Speed * Vector3.forward);

            movement.y -= m_Gravity * Time.deltaTime;
        }
        else
        {
            movement += m_CurrentClimbHand.m_Delta * m_ClimbSensitivity;
        }

        m_CharacterController.Move(movement * Time.deltaTime);
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

    /// <summary>
    /// Sets The Hand Thats Being Used For Climbing
    /// </summary>
    /// <param name="hand">Climbing Hand</param>
    public void SetHand(Hand hand)
    {
        if (m_CurrentClimbHand)
            m_CurrentClimbHand.Release();

        m_CurrentClimbHand = hand;
    }

    /// <summary>
    /// Clears The Climbing Hand 
    /// </summary>
    public void ClearHand()
    {
        m_CurrentClimbHand = null;
    }
}
