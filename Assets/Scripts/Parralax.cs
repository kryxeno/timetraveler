using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{

    private float length, startposX, startposY;
    public GameObject cam;
    public float parralaxEffect;
    public bool enableY = false;

    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = cam.transform.position.x * (1 - parralaxEffect);
        float distX = cam.transform.position.x * parralaxEffect;

        float distY = enableY ? cam.transform.position.y * parralaxEffect : 0;

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        if (tempX > startposX + length) startposX += length;
        else if (tempX < startposX - length) startposX -= length;
    }
}
