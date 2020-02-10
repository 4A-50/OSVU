using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum PickUpType { Hold, Toggle };
    public PickUpType holdType;

    [HideInInspector]
    public Hands activeHand;

    [HideInInspector]
    public Rigidbody rb = null;

    public Transform primaryAttachmentPoint;
    public Transform[] secondaryAttachmentsPoints;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public bool InHand()
    {
        if (activeHand != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
