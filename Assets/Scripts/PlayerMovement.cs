using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Ground movement")]
    [SerializeField] private float moveSpeed;
    private float _moveInput;

    [Header("Normal jumping")]
    [SerializeField] private float jumpForce;

    [SerializeField] private float jumpTime;
    private float _jumpTime;

    private bool isJumping;

    [SerializeField] private float hangTime;
    private float _hangTime;

    [SerializeField] private float jumpBufferLength;
    private float _jumpBufferLength;

    [Header("isGrounded?")]
    [SerializeField] private Transform feetPos;

    [SerializeField] private float checkRadius;

    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Physics")]
    [SerializeField] private float maxVelocityX = 10;
    [SerializeField] private float maxVelocityY = 10;
    private Rigidbody2D rb;

    [Header("Particels")]
    [SerializeField] private bool spawnDust;

    private bool _spawnJumpingDust;
    [SerializeField] private GameObject jumpingParticels;

    [SerializeField] private ParticleSystem footSteps;
    [SerializeField] private float footStepsSpeed;
    private ParticleSystem.EmissionModule footEmission;

    [Header("Animation")]
    private Animator anim;
    [SerializeField] private bool animationsActive;
    #endregion
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        footEmission = footSteps.emission;
    }
    private void Update()
    {
        Inputs();
        CheckWorld();

        if (animationsActive)
        {
            AnimationControl();
            Flip();
        }
    }
    private void FixedUpdate()
    {
        Jump();
        xMovement();
        rb.velocity = new Vector2(Math.Clamp(rb.velocity.x, -maxVelocityX, maxVelocityX), Math.Clamp(rb.velocity.y, -maxVelocityY, maxVelocityY));
    }
    void Inputs()
    {
        _moveInput = Input.GetAxis("Horizontal");
        //manage hangtime
        if (isGrounded)
        {
            _hangTime = hangTime;
        }
        else
        {
            _hangTime -= Time.deltaTime;
        }
        //manage jump buffer
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferLength = jumpBufferLength;
        }
        else
        {
            _jumpBufferLength -= Time.deltaTime;
        }

        //manage jump
        if (_jumpBufferLength >= 0 && _hangTime > 0)
        {
            isJumping = true;
            _jumpTime = jumpTime;
            _jumpBufferLength = 0;
            _hangTime = 0;
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (_jumpTime > 0)
            {

                _jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (!Input.GetButton("Jump"))
        {
            isJumping = false;
        }
    }
    void CheckWorld()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }
    void xMovement()
    {
        rb.velocity = new Vector2(_moveInput * moveSpeed, rb.velocity.y);
    }
    void Flip()
    {
        if (_moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (_moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    void Jump()
    {
        if (isJumping)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void AnimationControl()
    {
        if (isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (isJumping == true)
        {
            anim.SetBool("takeOff", true);
        }
        else
        {
            anim.SetBool("takeOff", false);
        }

        if (_moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }
    void Particles()
    {
        if (_moveInput != 0 && isGrounded)
        {
            footEmission.rateOverTime = footStepsSpeed;
        }
        else
        {
            footEmission.rateOverTime = 0;
        }

        if (_jumpBufferLength >= 0 && _hangTime > 0)
        {
            Instantiate(jumpingParticels, feetPos.position, feetPos.rotation);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
}
