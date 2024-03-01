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

    private float dirX = 0;
    public float moveSpeed = 8;
    public float jumpForce = 6;

    private enum MovementState { idle, running, jumping }
    private MovementState state = MovementState.idle;
    private bool doubleJumpAvailable = true;

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

        Debug.Log("grounded: " + grounded);
        Debug.Log("doubleJumpAvailable: " + doubleJumpAvailable);

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && (grounded || doubleJumpAvailable))
        {
            if (!grounded) doubleJumpAvailable = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState() {
        MovementState state;
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
        Vector2 bottomCenter = new Vector2(bounds.center.x, bounds.min.y);
        Vector2 bottomSize = new Vector2(bounds.size.x, bounds.size.y * 0.2f);

        return Physics2D.BoxCast(bottomCenter, bottomSize, 0f, Vector2.down, .1f, groundLayer);
    }
}
