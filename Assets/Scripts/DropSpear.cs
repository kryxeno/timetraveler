using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpear : MonoBehaviour
{
    public float dropDelay = 0.05f;

    void Drop()
    {
        StartCoroutine(DropWithDelay(dropDelay));
    }

    IEnumerator DropWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Drop();
    }
}
