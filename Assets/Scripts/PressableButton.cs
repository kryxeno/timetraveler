using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableButton : MonoBehaviour
{
    public float entrySpeed = 1f;
    private float relativeEntrySpeed;
    public float exitSpeed = 1f;
    private float relativeExitSpeed;
    private bool playerOn = false;
    public bool active = false;
    public bool activePermanently = false;
    public bool invert = false;

    private Vector3 originalPosition;
    private Vector3 newPosition;
    private Color originalColor;
    private float height;

    public List<GameObject> linkedPlatforms = new List<GameObject>();

    void Start()
    {
        height = this.GetComponent<SpriteRenderer>().bounds.size.y;
        originalPosition = transform.position;
        newPosition = new(originalPosition.x, originalPosition.y - height / 2, originalPosition.z);
        relativeEntrySpeed = entrySpeed;
        relativeExitSpeed = exitSpeed;
        originalColor = this.GetComponent<SpriteRenderer>().color;
    }

    void FixedUpdate()
    {
        if (playerOn && transform.position != newPosition)
        {
            MoveTo(newPosition, relativeEntrySpeed);
            relativeEntrySpeed += entrySpeed / 5;
        }
        else if (!playerOn && transform.position != originalPosition && !activePermanently)
        {
            MoveTo(originalPosition, relativeExitSpeed);
            relativeExitSpeed += exitSpeed / 5;
        }
        if ((transform.position.y < originalPosition.y - height / 4 && !active))
        {
            if (!active || activePermanently) UpdateLinkedObjects(true);
            active = true;
            FindObjectOfType<AudioManager>().Play("Click");
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (!activePermanently)
        {
            if (active) UpdateLinkedObjects(false);
            active = false;
            this.GetComponent<SpriteRenderer>().color = originalColor;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
            playerOn = true;
            relativeEntrySpeed = entrySpeed;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
            playerOn = false;
            relativeExitSpeed = exitSpeed;
        }
    }

    void MoveTo(Vector3 target, float _speed)
    {
        transform.position = Vector2.MoveTowards(transform.position,
        target, _speed * Time.deltaTime);
    }

    void UpdateLinkedObjects(bool newValue)
    {
        foreach (GameObject linkedPlatform in linkedPlatforms)
        {
            linkedPlatform.GetComponent<MovePlatform>().active = invert ? !newValue : newValue;
        }
    }
}
