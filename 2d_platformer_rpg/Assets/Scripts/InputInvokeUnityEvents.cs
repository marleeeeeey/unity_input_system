using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputInvokeUnityEvents : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;
    Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    public void MoveExample(InputAction.CallbackContext value)
    {
        direction = value.ReadValue<Vector2>();
    }

    public void AttackExample(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("Attack performed");
        }
        else if (value.canceled)
        {
            Debug.Log("Attack canceled");
        }
        else if (value.started)
        {
            Debug.Log("Attack started");
        }
    }
}
