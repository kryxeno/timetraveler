using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordObject : MonoBehaviour
{
    //place this script on the player gameobject

    public GameObject ghost;
    public PlayerMovement playerMovement;
    public bool ghostEnabled = true;
    public float delaySeconds = 5f;
    public List<Vector3> storedPositions;
    public List<bool> storedDirections;
    public List<Sprite> storedSprites;

    public GameObject currentGhost;
    public float updateInterval = 0.1f;
    public float timeSinceReset = 0f;
    private IEnumerator updateCoroutine;

    public class FrameData
    {
        public Vector3 position;
        public Sprite sprite;
        public bool flipX;
        public bool doubleJumpAvailable;
    }

    public List<FrameData> storedFrames = new();

    void Start()
    {
        updateCoroutine = UpdateGhost();
        StartCoroutine(updateCoroutine);
    }

    void Update()
    {
        timeSinceReset += Time.deltaTime;
    }


    public int GetMaxFrames()
    {
        return Mathf.CeilToInt(delaySeconds / updateInterval);
    }

    private IEnumerator UpdateGhost()
    {
        while (ghostEnabled)
        {
            if (currentGhost == null)
            {
                currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                currentGhost.GetComponent<SpriteRenderer>().enabled = false;
            }

            if (timeSinceReset >= delaySeconds)
            {
                currentGhost.GetComponent<SpriteRenderer>().enabled = true;
                UpdateGhostPosition();
                RemoveOldestFrame();
            }

            AddNewFrame();

            yield return new WaitForSeconds(updateInterval);
        }
    }

    public void ResetGhost()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }

        timeSinceReset = 0f;

        storedFrames = new List<FrameData>();
    }

    public void DisableGhost()
    {
        ResetGhost();
        ghostEnabled = false;
    }

    public void UpdateGhostPosition()
    {
        if (currentGhost != null)
        {
            currentGhost.transform.position = storedFrames[0].position; // Move
            currentGhost.GetComponent<SpriteRenderer>().sprite = storedFrames[0].sprite;
            currentGhost.GetComponent<SpriteRenderer>().flipX = storedFrames[0].flipX;
        }
    }

    public void RemoveOldestFrame()
    {
        if (storedFrames.Count > 0) storedFrames.RemoveAt(0);
    }

    public void AddNewFrame()
    {
        FrameData frame = new()
        {
            position = transform.position,
            sprite = GetComponent<SpriteRenderer>().sprite,
            flipX = GetComponent<SpriteRenderer>().flipX,
            doubleJumpAvailable = playerMovement.doubleJumpAvailable
        };

        storedFrames.Add(frame);
    }

}

