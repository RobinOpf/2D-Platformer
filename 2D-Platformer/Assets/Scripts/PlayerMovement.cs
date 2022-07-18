using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    private float dirY = 0f;
    private bool isOnGround = false;
    [SerializeField] private int amountOfExtraJumps = 1;
    private int extraJumpsLeft;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling, crouching }

    //[SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        extraJumpsLeft = amountOfExtraJumps;
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        if (IsGrounded())
        {
            extraJumpsLeft = amountOfExtraJumps;
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
        if (dirY != -1)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        coll.offset = new Vector2(dirX * coll.offset.x, coll.offset.y);

        if (Input.GetButtonDown("Jump") && extraJumpsLeft > 0)
        {
            //jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJumpsLeft--;
        }

        IsGrounded();
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        flipChar();

        if (dirY < 0f)
        {
            state = MovementState.crouching;
        }
        else
        {
            if (dirX != 0f)
            {
                state = MovementState.running;
            }
            else
            {
                state = MovementState.idle;
            }
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
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void flipChar()
    {
        if (dirX > 0f)
        {
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            sprite.flipX = true;
        }

    }
}