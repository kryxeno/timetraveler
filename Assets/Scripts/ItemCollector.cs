using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public ControlTime controlTime;
    public List<GameObject> collectibles;
    public float keys = 0;
    public TextMeshProUGUI keyText;

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
        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Success");
            keys++;
            keyText.text = keys.ToString();
        }
    }

    public void SetKeys(float _keys)
    {
        this.keys = _keys;
        keyText.text = keys.ToString();
    }
}
