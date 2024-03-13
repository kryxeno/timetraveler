using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.name == "Deathbox" && other.gameObject.CompareTag("Ground")) ResetLevel();
        if (other.gameObject.CompareTag("Water"))
        {
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
