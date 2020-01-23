using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyBox;

public class Button : MonoBehaviour
{
    [Tag]
    public string Tag;

    [Space]
    public UnityEvent buttonPress;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag)
        {
            buttonPress.Invoke();
        }
    }
}
