using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{

    private float length, startposX, height, startposY;
    public GameObject cam;
    public float parralaxEffectX, parralaxEffectY;

    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = (cam.transform.position.x * (1 - parralaxEffectX));
        float distX = (cam.transform.position.x * parralaxEffectX);

        float tempY = (cam.transform.position.y * (1 - parralaxEffectY));
        float distY = (cam.transform.position.y * parralaxEffectY);

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        if (tempX > startposX + length) startposX += length;
        else if (tempX < startposX - length) startposX -= length;
    }
}
