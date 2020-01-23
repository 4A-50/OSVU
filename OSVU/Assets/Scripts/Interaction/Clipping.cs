using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Clipping : MonoBehaviour
{
    public enum Type {Box, Sphere, Capsule};
    public Type overlapType;

    [Space]

    [ConditionalField(nameof(overlapType), false, Type.Box)]
    public Vector3 boxExents;

    [ConditionalField(nameof(overlapType), false, Type.Sphere)]
    public float sphereRadius;

    [ConditionalField(nameof(overlapType), false, Type.Capsule)]
    public Vector3 capsule1;
    [ConditionalField(nameof(overlapType), false, Type.Capsule)]
    public Vector3 capsule2;
    [ConditionalField(nameof(overlapType), false, Type.Capsule)]
    public float capsuleRadius;

    [Space]

    public LayerMask layer;

    Vector3 lastPos;

    void Update()
    {
        switch (overlapType)
        {
            case Type.Box:
                if (Physics.OverlapBox(transform.position, boxExents, transform.rotation, layer).Length != 0)
                {
                    transform.position = lastPos;
                }
                break;
            case Type.Sphere:
                if (Physics.OverlapSphere(transform.position, sphereRadius, layer).Length != 0)
                {
                    transform.position = lastPos;
                }
                break;
            case Type.Capsule:
                if (Physics.OverlapCapsule(transform.position + capsule1, transform.position + capsule2, capsuleRadius, layer).Length != 0)
                {
                    transform.position = lastPos;
                }
                break;
        } 
    }

    void LateUpdate()
    {
        lastPos = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        switch (overlapType)
        {
            case Type.Box:
                Gizmos.DrawWireCube(transform.position, boxExents * 2);
                break;
            case Type.Sphere:
                Gizmos.DrawWireSphere(transform.position, sphereRadius);
                break;
            case Type.Capsule:
                DebugExtension.DrawCapsule(transform.position + capsule1, transform.position + capsule2, Color.red, capsuleRadius);
                break;
        }
    }
}
