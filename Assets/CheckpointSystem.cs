using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointSystem : MonoBehaviour
{
    public GameObject currentCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.gameObject;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void ReturnToCheckpoint()
    {
        this.transform.position = currentCheckpoint.transform.position;
    }

    public void ResetLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
