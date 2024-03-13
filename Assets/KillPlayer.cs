using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private CheckpointSystem checkpointSystem;

    private void Start()
    {
        checkpointSystem = this.GetComponent<CheckpointSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            checkpointSystem.ReturnToCheckpoint();
        }
    }

}
