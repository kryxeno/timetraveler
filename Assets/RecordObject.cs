using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordObject : MonoBehaviour
{
    //place this script on the player gameobject

    public GameObject ghost;
    public bool ghostEnabled = true;
    public float delaySeconds = 5f;
    public List<Vector3> storedPositions;
    public List<bool> storedDirections;
    public List<Sprite> storedSprites;

    public GameObject currentGhost;
    public float updateInterval = 0.1f;
    public float timeSinceReset = 0f;
    private IEnumerator updateCoroutine;

    void Awake()
    {
        storedPositions = new List<Vector3>(); //create a blank list
        storedSprites = new List<Sprite>(); //create a blank list
        storedDirections = new List<bool>(); //create a blank list
    }

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

        storedPositions = new List<Vector3>();
        storedSprites = new List<Sprite>();
        storedDirections = new List<bool>();
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
            currentGhost.transform.position = storedPositions[0]; // Move
            currentGhost.GetComponent<SpriteRenderer>().sprite = storedSprites[0];
            currentGhost.GetComponent<SpriteRenderer>().flipX = storedDirections[0];
        }
    }

    public void RemoveOldestFrame()
    {
        if (storedPositions.Count > 0)
        {
            storedPositions.RemoveAt(0);
            storedSprites.RemoveAt(0);
            storedDirections.RemoveAt(0);
        }
    }

    public void AddNewFrame()
    {
        storedPositions.Add(transform.position);
        storedSprites.Add(GetComponent<SpriteRenderer>().sprite);
        storedDirections.Add(GetComponent<SpriteRenderer>().flipX);
    }

}

