using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using MyBox;

public class Hand : MonoBehaviour
{
    //The SteamVR Action Needed For A Grab
    public SteamVR_Action_Boolean grabAction = null;

    //The Interactable Tag
    [Tag]
    public string interactableTag;

    //The Hands Delta (Used For Climbing)
    public Vector3 delta = Vector3.zero; // { private set; get; }
    //Hands Last Position
    Vector3 lastPosition = Vector3.zero;

    //Hand Controllers
    [HideInInspector]
    public SteamVR_Behaviour_Pose pose = null;
    SteamVR_Behaviour_Skeleton skeleton = null;
    FixedJoint joint = null;

    //Current In Hand Interactable
    Interactable currentInteractable = null;
    //All The Nearby Interactables
    List<Interactable> contactInteractables = new List<Interactable>();
    //Nearest Interactable
    Interactable nearestInteractable = null;

    //The Controller Script
    VRController vrController = null;

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        skeleton = GetComponent<SteamVR_Behaviour_Skeleton>();
        joint = GetComponent<FixedJoint>();
        vrController = transform.parent.GetComponentInParent<VRController>();
    }

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (contactInteractables.Count > 0)
        {
            nearestInteractable = GetNearestInteractable();

            if (nearestInteractable.climbable == false)
            {
                if (grabAction.GetStateDown(pose.inputSource) && nearestInteractable.holdType == Interactable.PickUpType.Toggle)
                {
                    if (nearestInteractable.activeHand == null)
                    {
                        Pickup();
                    }
                    else
                    {
                        Drop();
                    }
                }
                else if (nearestInteractable.holdType == Interactable.PickUpType.Hold)
                {
                    if (grabAction.GetStateDown(pose.inputSource))
                    {
                        Pickup();
                    }

                    if (grabAction.GetStateUp(pose.inputSource))
                    {
                        Drop();
                    }
                }
            }
            else
            {
                if (grabAction.GetStateDown(pose.inputSource))
                {
                    Grab();
                }

                if (grabAction.GetStateUp(pose.inputSource))
                {
                    Release();
                }
            }
        }
    }

    #region Climbing Delta
    private void FixedUpdate()
    {
        lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        delta = lastPosition - transform.position;
    }
    #endregion

    /// <summary>
    /// Adds Any Nearby Interactables Into The List Of Them.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(interactableTag))
            return;

        contactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    /// <summary>
    /// Removes Any Interacatbles From The List When They Get Too Far Away.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(interactableTag))
            return;

        contactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    /// <summary>
    /// Used To Pickup Objects.
    /// <para>
    /// First Drops One If Its In The Hand Then Sets It's Location To The Hands,
    /// Then Connects It Using The Joint,
    /// Then It Sets It As The Current Interactable.
    /// </para>
    /// </summary>
    public void Pickup()
    {
        /*if (!currentInteractable)
            return;

        if (currentInteractable.activeHand)
            currentInteractable.activeHand.Drop();*/

        currentInteractable = nearestInteractable;
        nearestInteractable = null;

        currentInteractable.transform.position = transform.position;

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;

        currentInteractable.activeHand = this;
    }

    /// <summary>
    /// Used To Drop Objects.
    /// <para>
    /// First It Checks Thers Actually And Interactable There,
    /// Then It Works Out The Force To Apply,
    /// Then Drops The Object.
    /// </para>
    /// </summary>
    public void Drop()
    {
        if (!currentInteractable)
            return;

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();

        joint.connectedBody = null;

        currentInteractable.activeHand = null;
        currentInteractable = null;
    }

    public void Grab()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        vrController.movementType = VRController.MovementType.Climbing;
        vrController.SetHand(this);
    }

    public void Release()
    {
        vrController.ClearHand();
        vrController.movementType = VRController.MovementType.Ground;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    /// <summary>
    /// Returns The Nearest Interactable.
    /// <para>
    /// Uses Square Magnitude To Find The Close Interactable In The List.
    /// </para>
    /// </summary>
    /// <returns>The Nearest Interactable</returns>
    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0f;

        foreach (Interactable interactable in contactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
