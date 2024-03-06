using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public float ghostDelay;
    private float ghostDelaySeconds;
    private float deactivateDelay = 1f;
    public GameObject ghost;
    public bool makeGhost = false;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost == true)
        {
            ShowGhostInstance();
        }
    }

    private void ShowGhostInstance()
    {
        if (ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.deltaTime;
        }
        else
        {
            GameObject currentGhost = ObjectPool.SharedInstance.GetPooledObject();
            if (currentGhost != null)
            {
                currentGhost.transform.position = transform.position;
                currentGhost.transform.rotation = transform.rotation;
                currentGhost.SetActive(true);
            }
            Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
            currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
            currentGhost.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
            ghostDelaySeconds = ghostDelay;

            StartCoroutine(DeactivateGhostAfterDelay(currentGhost, deactivateDelay));
        }
    }
    // Coroutine to delay deactivating the ghost
    private IEnumerator DeactivateGhostAfterDelay(GameObject ghost, float delay)
    {
        yield return new WaitForSeconds(delay);
        ghost.SetActive(false);
    }
}
