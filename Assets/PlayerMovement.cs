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

    private float dirX = 0;
    public float moveSpeed = 8;
    public float jumpForce = 6;

    private enum MovementState { idle, running, jumping }
    private MovementState state = MovementState.idle;

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
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()
        )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        Debug.Log(rb.velocity);

        UpdateAnimationState();
    }

    private void UpdateAnimationState() {
        MovementState state;
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
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
}
