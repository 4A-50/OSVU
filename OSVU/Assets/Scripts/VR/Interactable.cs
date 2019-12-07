using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using MyBox;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    //The Type Of Interactable This Is
    public enum InteractableType { Throwable, Object, ClimbingPoint, Gun };
    public InteractableType Type;

    //How It's Picked Up
    public enum PickUpType { Hold, Toggle };
    public PickUpType holdType;

    //Hand Its Being Held By
    [HideInInspector]
    public Hand activeHand = null;

    //This Objects Rigidbody
    private Rigidbody rb = null;

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
    //Gun's Fire Modes
    public enum FireMode { Single, Burst, FullAuto };
    [ConditionalField(nameof(Type), false, InteractableType.Gun)] public FireMode fireMode;
    //Guns Fire Rate (If Its Not In Single Shot
    [ConditionalField(nameof(fireMode), true, FireMode.Single)] public float fireRate = 0.5f;   
    //The Guns Magazine Tag
    [ConditionalField(nameof(Type), false, InteractableType.Gun)] public string magazineTag;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (Type == InteractableType.ClimbingPoint)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    /// <summary>
    /// Updates Some Vars When They Are Changed In The Inspector.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (Type == InteractableType.Object)
        {
            if (holdType != PickUpType.Toggle)
                holdType = PickUpType.Toggle;

        }
        else if (Type == InteractableType.Gun)
        {
            if (holdType != PickUpType.Toggle)
                holdType = PickUpType.Toggle;
        }
        else
        {
            if (holdType != PickUpType.Hold)
                holdType = PickUpType.Hold;
        }
    }
}
