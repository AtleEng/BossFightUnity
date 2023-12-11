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

    [SerializeField] Transform weaponHand;

    [Header("Animation")]
    private Animator anim;

    Camera cam;
    #endregion
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        cam = Camera.main;
    }
    private void Update()
    {
        Inputs();
        CheckWorld();
        RotateHand();
        Flip();

        anim.SetFloat("FallSpeed", rb.velocity.y);
        anim.SetFloat("MoveSpeed", Math.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
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
    void RotateHand()
    {
        Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition) - weaponHand.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //omvandlar en vinkel till rotation
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //sätter rotationen på vapnet
        weaponHand.rotation = rotation;
    }
    void Flip()
    {
        Vector2 v = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (v.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            weaponHand.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (v.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponHand.localScale = new Vector3(-1f, -1f, 1f);
        }
    }
    void Jump()
    {
        if (isJumping)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
}
