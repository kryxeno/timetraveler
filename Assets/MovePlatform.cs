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
        if (transform.position == pathStart && (!active || continuous))
        {
            //If current location of platform is start position,
            target = pathEnd; // set end position as target.
        }
        else if (transform.position == pathEnd && (active || continuous))
        {
            //If current location of platform is end position,
            target = pathStart; // set start position as target.
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