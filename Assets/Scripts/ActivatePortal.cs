using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ActivatePortal : MonoBehaviour
{
    public GameObject portal;
    public TMPro.TextMeshProUGUI activateFireText;
    public TMPro.TextMeshProUGUI enterPortalText;
    public Animator portalAnimator;
    private bool canInteract = false;
    private bool portalActive = false;
    private bool animationCompleted = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (portalActive)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                portalActive = true;
                activateFireText.gameObject.SetActive(false);
                portal.SetActive(true);
                portalAnimator.SetTrigger("Activate");
            }
        }
    }

    public void OnAnimationComplete()
    {
        Debug.Log("Activating portal text");

        animationCompleted = true;
        enterPortalText.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;
            if (portalActive)
            {
                activateFireText.gameObject.SetActive(false);
                enterPortalText.gameObject.SetActive(true);
            }
            else
            {
                activateFireText.gameObject.SetActive(true);
                enterPortalText.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            if (activateFireText) activateFireText.gameObject.SetActive(false);
            if (enterPortalText) enterPortalText.gameObject.SetActive(false);
        }
    }
}
