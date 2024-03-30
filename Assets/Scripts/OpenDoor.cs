using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public bool open = false;
    public bool openPermanently = true;
    public bool locked = false;
    public TextMeshProUGUI lockedText;
    private bool canInteract = false;

    void Start()
    {
        if (open)
        {
            DoorAnimation(open);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            Debug.Log("E pressed");
            if (openPermanently && open) return;
            if (locked && !open)
            {
                float keys = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemCollector>().keys;
                if (keys == 0)
                {
                    FindObjectOfType<AudioManager>().Play("LockedDoor");
                    lockedText.gameObject.SetActive(true);
                    return;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<ItemCollector>().SetKeys(keys - 1);
                }
            };
            FindObjectOfType<AudioManager>().Play("DoorOpen");
            open = !open;
            DoorAnimation(open);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
    public void DoorAnimation(bool open)
    {
        // float doorWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x;
        // Vector3 doorPosition = transform.position;
        if (open)
        {
            // OpenDoorAnimation(transform, doorWidth, doorPosition);
            gameObject.SetActive(false);
        }
        else
        {
            // CloseDoorAnimation(transform, doorWidth, doorPosition);
            gameObject.SetActive(true);
        }
    }

    private void OpenDoorAnimation(Transform innerDoor, float doorWidth, Vector3 doorPosition)
    {
        innerDoor.localScale = new Vector3(innerDoor.localScale.x - doorWidth * 2, innerDoor.localScale.y, innerDoor.localScale.z);
        innerDoor.position = new Vector3(doorPosition.x - doorWidth, doorPosition.y, doorPosition.z);
        innerDoor.GetComponent<SpriteRenderer>().sortingLayerName = "-1";
        innerDoor.GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("Open the door");
    }

    private void CloseDoorAnimation(Transform innerDoor, float doorWidth, Vector3 doorPosition)
    {
        innerDoor.localScale = new Vector3(innerDoor.localScale.x + doorWidth * 2, innerDoor.localScale.y, innerDoor.localScale.z);
        innerDoor.position = new Vector3(doorPosition.x + doorWidth, doorPosition.y, doorPosition.z);
        innerDoor.GetComponent<BoxCollider2D>().enabled = true;
        innerDoor.GetComponent<SpriteRenderer>().sortingLayerName = "-2";
        Debug.Log("Close the door");
    }
}
