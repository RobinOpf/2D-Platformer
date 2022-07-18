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
    [SerializeField] private LayerMask wallJumpableWall;

    private float dirX = 0f;
    private float dirY = 0f;
    private bool isOnGround = false;
    private bool isFacingRight = true;
    [SerializeField] private int amountOfExtraJumps = 1;
    private int extraJumpsLeft;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private Vector3 standardCharScale;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpTime = 0.2f;
    [SerializeField] private float wallSlideSpeed = 0.3f;
    [SerializeField] private float wallDistance = 0.6f;
    [SerializeField] private float afterWallJumpDelay = 0.6f;
    private bool isWallSliding = false;
    private RaycastHit2D WallCheckHit;
    private float jumpTime;
    private float cantMoveTime = 0f;

    

    private enum MovementState { idle, running, jumping, falling, crouching }

    //[SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        standardCharScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        extraJumpsLeft = amountOfExtraJumps;
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        CheckIsGrounded();
        CheckIsFacingRight();
        if (isOnGround)
        {
            extraJumpsLeft = amountOfExtraJumps;
        }
        if (dirY != -1 && Time.time > cantMoveTime)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            FlipChar(isFacingRight);
        }
        coll.offset = new Vector2(dirX * coll.offset.x, coll.offset.y);


        //Wall Jump
        if (dirX > 0f)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, wallJumpableWall);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallJumpableWall);
        }

        if (WallCheckHit && !isOnGround && dirX != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isWallSliding)
            {
                cantMoveTime = Time.time + afterWallJumpDelay;
                rb.velocity = new Vector2(-Mathf.Sign(rb.velocity.x) * 5, jumpForce);
                FlipChar(!isFacingRight);
                extraJumpsLeft--;
                //jumpSoundEffect.Play();
            }
            else if (extraJumpsLeft > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                extraJumpsLeft--;

                //jumpSoundEffect.Play();
            }
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

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

    private void CheckIsGrounded()
    {
        isOnGround = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void CheckIsFacingRight()
    {
        if (Input.GetAxisRaw("Horizontal")>0)
        {
            isFacingRight = true;
        }
        else if (Input.GetAxisRaw("Horizontal")<0)
        {
            isFacingRight = false;
        }
    }

    private void FlipChar(bool facingright)
    {
        if (facingright)
        {
            transform.localScale = new Vector3(standardCharScale.x, transform.localScale.y, transform.localScale.z);
            //sprite.flipX = false;
        }
        else if (!facingright)
        {
            transform.localScale = new Vector3(-standardCharScale.x, transform.localScale.y, transform.localScale.z);
            //sprite.flipX = true;
        }

    }
}