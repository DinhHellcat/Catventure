using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    private GameManager gameManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (gameManager.IsGameOver()||gameManager.IsGameWin()) return;
        HandleMovement();
        handleJump();
        UpdateAnimation();
    }
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if(moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void handleJump()
    {
        if((Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w")) && isGrounded)
        {
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }    
}
