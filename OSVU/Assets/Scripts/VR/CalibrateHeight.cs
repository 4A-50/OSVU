using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CalibrateHeight : MonoBehaviour
{
    public Transform upperLeftArm, lowerLeftArm;
    public Transform upperRightArm, lowerRightArm;

    public float stepAmount = 0.05f;
    public float heightScale, armsScale;

    public void increaseHeight()
    {
        heightScale = transform.localScale.y + stepAmount;
        transform.localScale = new Vector3(heightScale, heightScale, heightScale);
    }

    public void decreaseHeight()
    {
        heightScale = transform.localScale.y - stepAmount;
        transform.localScale = new Vector3(heightScale, heightScale, heightScale);
    }

    public void increaseArms()
    {
        armsScale = lowerLeftArm.localScale.y + stepAmount;
        lowerLeftArm.localScale = upperLeftArm.localScale = lowerRightArm.localScale = lowerRightArm.localScale = new Vector3(heightScale, heightScale, heightScale);
    }

    public void decreaseArms()
    {
        armsScale = lowerLeftArm.localScale.y - stepAmount;
        lowerLeftArm.localScale = upperLeftArm.localScale = lowerRightArm.localScale = lowerRightArm.localScale = new Vector3(heightScale, heightScale, heightScale);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            increaseArms();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            decreaseArms();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            increaseHeight();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            decreaseHeight();
        }
    }
}
