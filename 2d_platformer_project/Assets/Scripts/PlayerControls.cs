using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 10f;

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
            rb.velocity = new Vector2(rb.velocity.x * moveSpeed, jumpForce);
            input.tryToJump = false;
        }
    }
}
