using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpaceLanes : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    public int laneLength = 15;

    [HideInInspector]
    public float min = 0;
    [HideInInspector]
    public int minLimit = 0;
    [HideInInspector]
    public float max = 45;
    [HideInInspector]
    public int maxLimit = 360;

    List<GameObject> path = new List<GameObject>();

    public void CreateStraight() {
        path.Clear();
        for (int i = 0; i < laneLength; i++)
        {
            if (i == 0)
            {
                GameObject Waypoint = Instantiate(prefab, transform.position, Quaternion.identity, transform);
                path.Add(Waypoint);
            }
            else
            {
                GameObject Waypoint = Instantiate(prefab, new Vector3(path[i - 1].transform.position.x + 30, path[i - 1].transform.position.y, path[i - 1].transform.position.z), Quaternion.identity, transform);
                path.Add(Waypoint);
            }
        }
    }

    public void CreateRoute()
    {
        path.Clear();
        for (int i = 0; i < laneLength; i++)
        {
            if (i == 0)
            {
                GameObject Waypoint = Instantiate(prefab, RandomPointInCircle(transform, 30, Random.Range(min, max)), Quaternion.identity, transform);
                path.Add(Waypoint);
            }
            else
            {
                GameObject Waypoint = Instantiate(prefab, RandomPointInCircle(path[i - 1].transform, 30, Random.Range(min, max)), Quaternion.identity, transform);
                path.Add(Waypoint);
            }
        }
    }

    Vector3 RandomPointInCircle(Transform trans, float radius, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector3 position = trans.right * Mathf.Sin(rad) + trans.forward * Mathf.Cos(rad);
        return trans.position + position * radius;
    }
}
