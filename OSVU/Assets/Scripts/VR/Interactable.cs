using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using MyBox;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    public enum InteractableType {Throwable, Object, ClimbingPoint, Gun};
    public InteractableType m_Type;

    [HideInInspector]
    public Hand m_ActiveHand = null;

    private Rigidbody m_Rigidbody = null;

    //Throwable Vars
    [ConditionalField(nameof(m_Type), false, InteractableType.Throwable)]
    public bool m_Bouncy = false;

    //Object Vars
    [ConditionalField(nameof(m_Type), false, InteractableType.Object)]
    public bool m_TogglePickUp = false;

    //Climbing Point Vars
    [ConditionalField(nameof(m_Type), false, InteractableType.ClimbingPoint)]
    public bool m_Climbable = false;

    //Gun Vars
    public enum FireMode {Single, Burst, FullAuto};
    [ConditionalField(nameof(m_Type), false, InteractableType.Gun)] public FireMode m_FireMode;
    [ConditionalField(nameof(m_FireMode), true, FireMode.Single)] public float m_FireRate = 0.5f;   
    [ConditionalField(nameof(m_Type), false, InteractableType.Gun)] public string m_MagazineTag;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        if (m_Type == InteractableType.ClimbingPoint)
        {
            m_Rigidbody.useGravity = false;
            m_Rigidbody.isKinematic = true;
        }
    }
}
