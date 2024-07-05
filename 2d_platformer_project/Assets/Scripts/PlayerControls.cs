using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private float rayLength;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private LayerMask detectLayer;
    private bool grounded;

    GatherInput2 input;
    Rigidbody2D rb;

    bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<GatherInput2>();
    }

    private void Update()
    {
        Flip();
    }

    // Frame-rate independent updates
    private void FixedUpdate()
    {
        GroundCheck();
        Move();
        Jump();
    }

    private void Move()
    {
        rb.velocity = new Vector2(input.valueX * moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        if (input.valueX > 0 && !facingRight || input.valueX < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void Jump()
    {
        if (input.tryToJump)
        {
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x * moveSpeed, jumpForce);
            }
            input.tryToJump = false;
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, detectLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, detectLayer);
        if (hitLeft || hitRight)
            grounded = true;
        else
            grounded = false;
    }
}
