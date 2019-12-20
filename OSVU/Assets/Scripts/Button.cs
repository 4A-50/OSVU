using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Button : MonoBehaviour
{
    [Tag]
    public string Tag;

    [SerializeField]
    Lift liftController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag)
        {
            liftController.Move();
        }
    }
}
