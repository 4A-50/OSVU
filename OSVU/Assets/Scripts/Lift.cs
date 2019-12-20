using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField]
    Vector3 bottomPos;
    [SerializeField]
    Vector3 topPos;

    [SerializeField]
    float speed = 1f;

    bool open = false;
    bool allowMoveUp;
    bool allowMoveDown;

    void Start()
    {
        transform.position = bottomPos;
    }
    
    void Update()
    {
        if (allowMoveDown == true)
        {
            if (transform.position != bottomPos)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, bottomPos, step);
            }
            else
            {
                allowMoveDown = false;
            }
        }

        if (allowMoveUp == true)
        {
            if (transform.position != topPos)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, topPos, step);
            }
            else
            {
                allowMoveUp = false;
            }
        }
    }

    public void Move()
    {
        if (open == true && transform.position == topPos)
        {
            open = false;
            allowMoveDown = true;
        }
        else if (open == false && transform.position == bottomPos)
        {
            open = true;
            allowMoveUp = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(bottomPos, 0.25f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(topPos, 0.25f);
    }
}
