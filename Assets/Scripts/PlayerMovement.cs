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
    public RecordObject recordObject;
    public ControlTime controlTime;

    private float dirX = 0;
    public float moveSpeed = 8;
    public float jumpForce = 6;

    private enum MovementState { idle, running, jumping, falling }
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
            FindObjectOfType<AudioManager>().Play("Jump");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ToggleGhost();
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0)
        {
            state = MovementState.running;
            sr.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sr.flipX = true;

        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        float extendDistance = 0.3f;
        Bounds bounds = bc.bounds;
        Vector2 bottomCenter = new Vector2(bounds.center.x, bounds.min.y);
        Vector2 raycastLeftOrigin = bottomCenter + new Vector2(-extendDistance, 0.0f); // Move origin slightly to the left
        Vector2 raycastRightOrigin = bottomCenter + new Vector2(extendDistance, 0.0f); // Move origin slightly to the right
        RaycastHit2D hitLeft = Physics2D.Raycast(raycastLeftOrigin, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(raycastRightOrigin, Vector2.down, 0.1f, groundLayer);
        return (hitLeft.collider != null || hitRight.collider != null);
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

    private void ToggleGhost()
    {
        controlTime.RevertTime();
    }
}

