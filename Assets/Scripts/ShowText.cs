using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    public List<TMP_Text> showAfterCollecting;

    private void Start()
    {
        if (showAfterCollecting.Count > 0)
        {
            foreach (TMP_Text text in showAfterCollecting)
            {
                text.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (showAfterCollecting.Count > 0)
        {
            foreach (TMP_Text text in showAfterCollecting)
            {
                text.enabled = true;
            }
        }
    }
}
