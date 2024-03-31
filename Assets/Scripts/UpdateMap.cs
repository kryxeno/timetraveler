using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMap : MonoBehaviour
{
    public GameObject cover;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cover.SetActive(false);
        }
    }
}
