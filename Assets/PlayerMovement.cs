using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private Animator anim;

    public LayerMask groundLayer;
    public Ghost ghost;

    public RecordObject recordObject;

    private float dirX = 0;
    public float moveSpeed = 8;
    public float jumpForce = 6;

    private enum MovementState { idle, running, jumping }
    private MovementState state = MovementState.idle;
    public bool doubleJumpAvailable = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        bool grounded = IsGrounded();

        if (grounded)
        {
            doubleJumpAvailable = true;
        }

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && (grounded || doubleJumpAvailable))
        {
            if (!grounded) doubleJumpAvailable = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            RevertTime();
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0)
        {
            state = MovementState.running;
            ghost.makeGhost = true;
            sr.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sr.flipX = true;
            ghost.makeGhost = true;

        }
        else
        {
            state = MovementState.idle;
            ghost.makeGhost = true;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        Bounds bounds = bc.bounds;
        Vector2 bottomCenter = new(bounds.center.x, bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(bottomCenter, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }


    private void RevertTime()
    {
        if (recordObject.timeSinceReset < recordObject.delaySeconds) return;

        transform.position = recordObject.storedFrames[0].position;
        GetComponent<SpriteRenderer>().sprite = recordObject.storedFrames[0].sprite;
        GetComponent<SpriteRenderer>().flipX = recordObject.storedFrames[0].flipX;
        doubleJumpAvailable = recordObject.storedFrames[0].doubleJumpAvailable;

        recordObject.ResetGhost();
    }
}

