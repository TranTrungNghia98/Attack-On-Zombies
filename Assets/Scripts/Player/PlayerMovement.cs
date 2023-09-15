using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float moveSpeed = 5;
    private float jumpForce = 5;

    // Ground Check
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    private float groundDistance = 0.4f;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Fixed Update
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        GroundCheck();
    }

    // Logic
    void CheckInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Physics
    void Move()
    {
        if (verticalInput != 0)
        {
            // Prevent rigidbody change gravity
            rb.velocity = transform.forward * verticalInput * moveSpeed + transform.up * rb.velocity.y;
        }
        
        if (horizontalInput != 0)
        {
            // Prevent rigidbody change gravity
            rb.velocity = transform.right * horizontalInput * moveSpeed + transform.up * rb.velocity.y;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
