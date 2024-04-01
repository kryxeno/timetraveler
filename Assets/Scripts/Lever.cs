using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private float rotateAngle = 25;
    private Transform handle;
    private Vector3 pivot;

    private bool active = false;
    private bool canInteract = false;

    public List<GameObject> linkedPlatforms = new List<GameObject>(); // List of platforms controlled by the lever
    private enum MovementState { off, on }
    private MovementState state = MovementState.off;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

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

    void UpdateLeverPosition()
    {
        active = !active;
        state = active ? MovementState.on : MovementState.off;
        FindObjectOfType<AudioManager>().Play(active ? "Click" : "ClickOff");

        anim.SetInteger("state", (int)state);

        updateLinkedObjects();

    }
    void updateLinkedObjects()
    {
        foreach (GameObject linkedPlatform in linkedPlatforms)
        {
            linkedPlatform.GetComponent<MovePlatform>().active = active;
        }
    }

}
