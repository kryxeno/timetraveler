using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public ControlTime controlTime;
    public List<GameObject> collectibles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);

            foreach (GameObject collectible in collectibles)
            {
                if (collectible.name == other.gameObject.name)
                {
                    collectible.SetActive(true);
                    FindObjectOfType<AudioManager>().Play("Success");
                    break;
                }
            }
        }
        if (other.gameObject.CompareTag("TimeCollectible"))
        {
            Destroy(other.gameObject);
            controlTime.SetControl(true);
            FindObjectOfType<AudioManager>().Play("Powerup");

        }
    }
}
