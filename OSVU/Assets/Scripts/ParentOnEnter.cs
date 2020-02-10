using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class ParentOnEnter : MonoBehaviour
{
    [Tag]
    public string tag;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
            other.transform.parent = transform;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == tag)
            other.transform.parent = null;
    }
}
