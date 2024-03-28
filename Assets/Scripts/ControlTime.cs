using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTime : MonoBehaviour
{

    public GameObject ghost;
    public PlayerMovement playerMovement;
    public bool controlEnabled = true;
    public bool ghostActive = false;
    public Ghost ghostScript;

    public GameObject currentGhost;

    public class FrameData
    {
        public Vector3 position;
        public Sprite sprite;
        public bool flipX;
        public bool doubleJumpAvailable;
    }

    public FrameData ghostPosition;

    private void UpdateGhostState()
    {
        if (!currentGhost)
        {
            currentGhost = Instantiate(ghost, transform.position, transform.rotation);
        }
        currentGhost.GetComponent<SpriteRenderer>().sprite = ghostPosition.sprite;
        currentGhost.GetComponent<SpriteRenderer>().flipX = ghostPosition.flipX;
        currentGhost.transform.position = ghostPosition.position;
    }

    public void SetControl(bool control)
    {
        controlEnabled = control;
        ghostActive = false;
        ghostScript.makeGhost = false;
    }

    public void RevertTime()
    {
        if (!controlEnabled) return;
        if (ghostActive)
        {
            ghostScript.makeGhost = false;
            playerMovement.transform.position = ghostPosition.position;
            playerMovement.GetComponent<SpriteRenderer>().sprite = ghostPosition.sprite;
            playerMovement.GetComponent<SpriteRenderer>().flipX = ghostPosition.flipX;
            playerMovement.doubleJumpAvailable = ghostPosition.doubleJumpAvailable;
            Destroy(currentGhost);
            ghostActive = false;
        }
        else
        {
            ghostScript.makeGhost = true;
            ghostPosition = new FrameData
            {
                position = playerMovement.transform.position,
                sprite = playerMovement.GetComponent<SpriteRenderer>().sprite,
                flipX = playerMovement.GetComponent<SpriteRenderer>().flipX,
                doubleJumpAvailable = playerMovement.doubleJumpAvailable
            };
            UpdateGhostState();
            ghostActive = true;
        }
    }

}
