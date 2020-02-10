using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicHands : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Transform vrTracker;

    [SerializeField]
    Transform ikLocation;

    public float multiplier = 10;

    void FixedUpdate()
    {
        Vector3 movePos = vrTracker.position - transform.position;
        rb.velocity = movePos * multiplier;

        rb.MoveRotation(vrTracker.rotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, vrTracker.position);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, ikLocation.position);
    }
}
