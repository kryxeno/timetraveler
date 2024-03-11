using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int collectibles = 0;

    public Text collectibleText;

    public RecordObject recordObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            collectibles++;
            collectibleText.text = "Collectibles: " + collectibles.ToString();
        }
        if (other.gameObject.CompareTag("TimeCollectible"))
        {
            Destroy(other.gameObject);
            recordObject.EnableGhost();
        }
    }
}
