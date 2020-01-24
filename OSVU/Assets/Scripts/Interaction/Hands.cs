using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using MyBox;

public class Hands : MonoBehaviour
{
    [SerializeField]
    SteamVR_Action_Boolean grabAction;

    [Tag]
    public string interactableTag;

    List<Interactable> nearbyInteractables = new List<Interactable>();
    Interactable nearestInteractable;

    public Vector3 delta { private set; get; } = Vector3.zero;
    Vector3 lastPosition = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        nearestInteractable = GetNearestInteractable();


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
}