using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordObject : MonoBehaviour
{
    //place this script on the player gameobject

    public GameObject ghost;
    public float delaySeconds = 5f;
    public List<Vector3> storedPositions;
    public List<bool> storedDirections;
    public List<Sprite> storedSprites;

    private float maxFrames;

    public GameObject currentGhost;
     public float updateInterval = 0.1f;
     public float timeSinceLastUpdate = 0f;
         private IEnumerator updateCoroutine;

    void Awake()
    {
        storedPositions = new List<Vector3>(); //create a blank list
        storedSprites = new List<Sprite>(); //create a blank list
        storedDirections = new List<bool>(); //create a blank list

        maxFrames = delaySeconds * 60;
    }

    void Start()
    {
                updateCoroutine = UpdateGhost();
        StartCoroutine(updateCoroutine);
    }

    void Update()
    {

    }


    public int GetMaxFrames()
    {
        return Mathf.CeilToInt(delaySeconds / updateInterval);
    }

     private IEnumerator UpdateGhost()
    {
        while (true)
        {
            if (currentGhost == null)
            {
                currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                currentGhost.GetComponent<SpriteRenderer>().enabled = false;
            }

            if (timeSinceLastUpdate >= delaySeconds)
            {
                currentGhost.GetComponent<SpriteRenderer>().enabled = true;
                currentGhost.transform.position = storedPositions[0]; // Move
                currentGhost.GetComponent<SpriteRenderer>().sprite = storedSprites[0];
                currentGhost.GetComponent<SpriteRenderer>().flipX = storedDirections[0];
                storedPositions.RemoveAt(0);
                storedSprites.RemoveAt(0);
                storedDirections.RemoveAt(0);
            }

            storedPositions.Add(transform.position);
            storedSprites.Add(GetComponent<SpriteRenderer>().sprite);
            storedDirections.Add(GetComponent<SpriteRenderer>().flipX);
            timeSinceLastUpdate += Time.deltaTime;

            yield return new WaitForSeconds(updateInterval);
        }
    }

    public void ResetGhost()
    {
        if (currentGhost != null)
        {
                    storedPositions = new List<Vector3>();
        storedSprites = new List<Sprite>();
        storedDirections = new List<bool>();
        Destroy(currentGhost);
        }
    }
}

