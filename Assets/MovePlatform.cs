using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 pathStart;
    private Vector3 pathEnd;
    private Vector3 target;

    public bool active = true;

    public bool continuous = false;



    void Start()
    {
        pathStart = transform.position;
        //Set start position of platform at location of platform
        pathEnd = transform.GetChild(0).position;
        //Set end position of platform at location of child (PathEnd)
        target = pathEnd;
    }
    void FixedUpdate()
    {
        if (continuous)
        {
            if (transform.position == pathStart)
            {
                target = pathEnd;
            }
            else if (transform.position == pathEnd)
            {
                target = pathStart;
            }
        }
        else if (active)
        {
            target = pathEnd;
        }
        else
        {
            target = pathStart;
        }

        bool continuousCanMove = continuous && active;
        bool notContinuousCanMove = !continuous && (active && transform.position != pathEnd || !active && transform.position != pathStart);
        if (continuousCanMove || notContinuousCanMove) Move();
    }

    void Move()
    {

        transform.position = Vector2.MoveTowards(transform.position,
        target, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { //If player is on moving platform,
            other.transform.parent = transform;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}