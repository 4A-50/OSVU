using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LandingRack : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void openRack()
    {
        anim.SetTrigger("Open");
        anim.ResetTrigger("Close");
    }

    public void closeRack()
    {
        anim.ResetTrigger("Open");
        anim.SetTrigger("Close");
    }
}
