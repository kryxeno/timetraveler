using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject player;
    public float rotateAngle = 25;
    private Vector3 pivot;

    private bool active = false;
    private bool canInteract = false;

    public List<GameObject> linkedPlatforms = new List<GameObject>(); // List of platforms controlled by the lever


    void Start()
    {
        pivot = transform.GetChild(0).position;
        transform.RotateAround(pivot, Vector3.forward, rotateAngle * (active ? -1 : 1));
        updateLinkedObjects();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            Debug.Log("E pressed");
            UpdateLeverPosition();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            canInteract = false;
        }
    }

    void UpdateLeverPosition()
    {
        // Rotate the lever
        if (pivot != null)
        {
            active = !active;
            if (active)
            {
                transform.RotateAround(pivot, Vector3.forward, rotateAngle * -2);
            }
            else
            {
                transform.RotateAround(pivot, Vector3.forward, rotateAngle * 2);
            }

            updateLinkedObjects();
        }
        else
        {
            Debug.LogError("Lever pivot not found!");
        }
    }
    void updateLinkedObjects()
    {
        foreach (GameObject linkedPlatform in linkedPlatforms)
        {
            linkedPlatform.GetComponent<MovePlatform>().active = active;
        }
    }

}
