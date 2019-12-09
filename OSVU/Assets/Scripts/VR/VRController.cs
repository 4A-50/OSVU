using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float sensitivity = 0.1f;
    public float maxSpeed = 0.1f;
    public float gravity = 200f;
    public float climbSensitivity = 45f;

    public enum MovementType {Ground, Climbing, Flying};
    [HideInInspector]
    public MovementType movementType;

    [SerializeField]
    Hand currentClimbHand = null;

    public SteamVR_Action_Vector2 moveValue = null;

    float speed = 0.0f;

    CharacterController characterController = null;
    Transform cameraRig = null;
    Transform head = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }
    
    private void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    /// <summary>
    /// Handles The Head Rotation.
    /// <para>
    /// Starts By Saving The Current Pos And Rot,
    /// Then Rotates The GameObject,
    /// Then Resets The Camera Rig To The Previous Position.
    /// </para>
    /// </summary>
    private void HandleHead()
    {
        Vector3 oldPosition = cameraRig.position;
        Quaternion oldRotation = cameraRig.rotation;

        transform.eulerAngles = new Vector3(0, head.rotation.eulerAngles.y, 0);

        cameraRig.position = oldPosition;
        cameraRig.rotation = oldRotation;
    }

    /// <summary>
    /// Calculates The Movement Amount And Them Moves The Character Controller.
    /// <para>
    /// Starts By Working Out Where The Headset Is Facing,
    /// Then Checks If The Buttons Been Pressed,
    /// If It Has It Works Out The Move And Moves It.
    /// </para>
    /// <para>
    /// If The Player Is Climbing Then It Works Out The Hands
    /// Delta And Multiplys That By The Sensitivity, And Moves
    /// The Player.
    /// </para>
    /// </summary>
    private void CalculateMovement()
    {
        if (movementType == MovementType.Ground)
        {
            float rotation = Mathf.Atan2(moveValue.axis.x, moveValue.axis.y);
            rotation *= Mathf.Rad2Deg;
            Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y + rotation, 0);
            Quaternion orientation = Quaternion.Euler(orientationEuler);
            Vector3 movement = Vector3.zero;

            if (moveValue.axis.magnitude == 0)
                speed = 0;


            speed += moveValue.axis.magnitude * sensitivity;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            movement += orientation * (speed * Vector3.forward);

            movement.y -= gravity * Time.deltaTime;
            characterController.Move(movement * Time.deltaTime);
        }
        else if(movementType == MovementType.Climbing)
        {
            Vector3 movement = Vector3.zero;

            movement += currentClimbHand.delta * climbSensitivity;

            characterController.Move(movement * Time.deltaTime);
        }
    }

    /// <summary>
    /// Works Out The Distance From The Headset To The Ground, Then Sets The CC's Hieght To It.
    /// <para>
    /// Halfs The Height Then Recenters It,
    /// Then It Aligns The CC To The Correct Position.
    /// </para>
    /// </summary>
    private void HandleHeight()
    {
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        characterController.center = newCenter;
    }

    /// <summary>
    /// Sets The Hand Thats Being Used For Climbing
    /// </summary>
    /// <param name="hand">Climbing Hand</param>
    public void SetHand(Hand hand)
    {
        if (currentClimbHand)
            currentClimbHand.Release();

        currentClimbHand = hand;
    }

    /// <summary>
    /// Clears The Climbing Hand 
    /// </summary>
    public void ClearHand()
    {
        currentClimbHand = null;
    }
}
