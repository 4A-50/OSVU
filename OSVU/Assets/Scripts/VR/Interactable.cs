using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Interactable : MonoBehaviour
{
    public enum InteractableType {Throwable, Object, ClimbingPoint};
    public InteractableType m_Type;

    [HideInInspector]
    public Hand m_ActiveHand = null;

    public bool m_TogglePickUp = false;

    public bool m_Climbable = false;
}
