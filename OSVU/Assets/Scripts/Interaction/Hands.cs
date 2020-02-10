using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using MyBox;

public class Hands : MonoBehaviour
{
    public enum HandOptions {Left, Right};
    public HandOptions handType;
    SteamVR_Input_Sources inputSource;

    [SerializeField]
    SteamVR_Action_Boolean grabAction;

    [Tag]
    public string interactableTag;

    List<Interactable> nearbyInteractables = new List<Interactable>();
    Interactable nearestInteractable;
    Interactable currentInteractable;

    public Vector3 delta {private set; get;} = Vector3.zero;
    Vector3 lastPosition = Vector3.zero;

    [SerializeField]
    Rigidbody rb;

    public FixedJoint joint;

    void Start()
    {
        
    }

    void Update()
    {
        setUpHandType();

        if (nearbyInteractables.Count > 0)
        {
            nearestInteractable = GetNearestInteractable();

            switch (nearestInteractable.holdType)
            {
                case Interactable.PickUpType.Hold:
                    if (grabAction.GetStateDown(inputSource))
                    {
                        Pickup();
                    }

                    if (grabAction.GetStateUp(inputSource))
                    {
                        Drop();
                    }
                    break;

                case Interactable.PickUpType.Toggle:
                    if (grabAction.GetStateDown(inputSource))
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
                    break;
            }
        }
    }

    #region Hand Delta
    void FixedUpdate()
    {
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        delta = lastPosition - transform.position;
    }
    #endregion

    public void Pickup()
    {
        currentInteractable = nearestInteractable;
        nearestInteractable = null;

        currentInteractable.transform.position = transform.position;

        Rigidbody targetBody = currentInteractable.rb;
        joint.connectedBody = targetBody;

        currentInteractable.activeHand = this;
    }

    public void Drop()
    {
        if (!currentInteractable)
            return;

        Rigidbody targetBody = currentInteractable.rb;
        targetBody.velocity = rb.velocity;
        targetBody.angularVelocity = rb.angularVelocity;

        joint.connectedBody = null;

        currentInteractable.activeHand = null;
        currentInteractable = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != interactableTag)
            return;

        nearbyInteractables.Add(other.GetComponent<Interactable>());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != interactableTag)
            return;

        nearbyInteractables.Remove(other.GetComponent<Interactable>());
    }

    Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0f;

        foreach (Interactable interactable in nearbyInteractables)
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

    void setUpHandType()
    {
        switch (handType)
        {
            case HandOptions.Left:
                inputSource = SteamVR_Input_Sources.LeftHand;
                break;

            case HandOptions.Right:
                inputSource = SteamVR_Input_Sources.RightHand;
                break;
        }
    }
}