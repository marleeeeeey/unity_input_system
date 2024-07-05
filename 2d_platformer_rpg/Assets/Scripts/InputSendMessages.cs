using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSendMessages : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    void OnMove(InputValue value)
    {
        Debug.Log("OnMove: " + value.Get<Vector2>());
        direction = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        Debug.Log("OnAttack: " + value.Get<float>());
    }
}
