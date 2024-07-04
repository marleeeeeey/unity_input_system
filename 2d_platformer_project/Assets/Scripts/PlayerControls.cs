using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GatherInput2 input;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;

    bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Flip();
    }

    // Frame-rate independent updates
    private void FixedUpdate()
    {
        Move();
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
}
