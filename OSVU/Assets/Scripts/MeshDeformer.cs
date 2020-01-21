using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer meshRenderer;
    [SerializeField]
    MeshCollider collider;

    float time;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.25f)
        {
            time = 0;
            UpdateCollider();
        }
    }

    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        collider.sharedMesh = null;
        collider.sharedMesh = colliderMesh;
    }
}
