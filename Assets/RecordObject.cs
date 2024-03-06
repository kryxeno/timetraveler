using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordObject : MonoBehaviour
{
    //place this script on the player gameobject

    public GameObject ghost;
    public float delaySeconds = 5;
    public List<Vector3> storedPositions;
    public List<bool> storedDirections;
    public List<Sprite> storedSprites;

    private float maxFrames;

    public GameObject currentGhost;

    void Awake()
    {
        storedPositions = new List<Vector3>(); //create a blank list
        storedSprites = new List<Sprite>(); //create a blank list
        storedDirections = new List<bool>(); //create a blank list

        maxFrames = delaySeconds * 60;
    }

    void Start()
    {
    }

    void Update()
    {
        if (currentGhost == null)
        {
            currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            currentGhost.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (storedPositions.Count > maxFrames)
        {
            currentGhost.GetComponent<SpriteRenderer>().enabled = true;
            currentGhost.transform.position = storedPositions[0]; //move
            currentGhost.GetComponent<SpriteRenderer>().sprite = storedSprites[0];
            currentGhost.GetComponent<SpriteRenderer>().flipX = storedDirections[0];
            storedPositions.RemoveAt(0);
            storedSprites.RemoveAt(0);
            storedDirections.RemoveAt(0);
            return;
        }

        storedPositions.Add(transform.position); //store the position every frame
        storedSprites.Add(GetComponent<SpriteRenderer>().sprite);
        storedDirections.Add(GetComponent<SpriteRenderer>().flipX);

    }
}

