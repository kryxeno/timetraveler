using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiggle : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveSpeed = 0.5f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * moveSpeed, moveDistance) / moveDistance;
        t = Mathf.SmoothStep(0f, 1f, t);
        float newYPosition = Mathf.Lerp(initialPosition.y - moveDistance / 2f, initialPosition.y + moveDistance / 2f, t);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}
