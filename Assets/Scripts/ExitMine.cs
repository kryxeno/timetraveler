using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExitMine : MonoBehaviour
{
    public TMPro.TextMeshProUGUI exitMineText;
    private bool canInteract = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;
            exitMineText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            exitMineText.gameObject.SetActive(false);
        }
    }
}
