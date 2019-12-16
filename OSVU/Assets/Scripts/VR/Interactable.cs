using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using MyBox;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    //The Type Of Interactable This Is
    public enum InteractableType {Throwable, Object, ClimbingPoint, Gun};
    public InteractableType Type;

    //How It's Picked Up
    public enum PickUpType {Hold, Toggle};
    public PickUpType holdType;

    //Hand Its Being Held By
    [HideInInspector]
    public Hand activeHand = null;

    //This Objects Rigidbody
    Rigidbody rb = null;

    //The Objects Skeleton Poser
    public SteamVR_Skeleton_Poser skelPoser = null;
    
    #region Throwable Vars
    //Is The Object Bouncy
    [ConditionalField(nameof(Type), false, InteractableType.Throwable)]
    public bool bouncy = false;
    #endregion

    #region Object Vars
    #endregion

    #region Climbing Point Vars
    //Is This Climbing Point Actually Climbable
    [ConditionalField(nameof(Type), false, InteractableType.ClimbingPoint)]
    public bool climbable = false;
    #endregion

    #region Gun Vars
    //Gun Types
    public enum GunType {Bullet, Lazer};
    [ConditionalField(nameof(Type), false, InteractableType.Gun)] public GunType gunType;
    //Gun's Fire Modes
    public enum FireMode {Single, Burst, FullAuto};
    [ConditionalField(nameof(Type), false, InteractableType.Gun)] public FireMode fireMode;
    //The Steam VR Action To Fire The Gun
    [ConditionalField(nameof(Type), false, InteractableType.Gun)] public SteamVR_Action_Boolean shootAction = null;
    //Guns Fire Rate (If Its Not In Single Shot
    [ConditionalField(nameof(fireMode), true, FireMode.Single)] public float fireRate = 0.5f;   
    //The Guns Magazine Tag
    [ConditionalField(nameof(Type), false, InteractableType.Gun)] public string magazineTag;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (Type == InteractableType.ClimbingPoint)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (Type == InteractableType.Gun && activeHand != null)
        {
            if (shootAction.GetStateDown(activeHand.pose.inputSource))
            {
                print("Shoot");
            }
        }
    }
}
