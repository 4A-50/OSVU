using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hands activeHand;

    Rigidbody rb = null;

    public Transform primaryAttachmentPoint;
    public Transform[] secondaryAttachmentsPoints;

    void Start()
    {
        
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
