using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathbox : MonoBehaviour
{

    private GameObject player;
    private CheckpointSystem checkpointSystem;

    private void Start()
    {
        player = this.transform.parent.gameObject;
        checkpointSystem = player.GetComponent<CheckpointSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground")) checkpointSystem.ReturnToCheckpoint();
    }
}
