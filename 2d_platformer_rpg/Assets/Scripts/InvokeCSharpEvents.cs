using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvokeCSharpEvents : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    // Components
    Rigidbody2D rb;
    PlayerInput playerInput;
    Animator animator;

    // Input
    InputActionMap playerBasicInputMap;
    InputAction attackAction;
    InputAction moveAction;

    // States
    Vector2 direction;

    private void Awake()
    {
        // Components
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        // Input
        playerBasicInputMap = playerInput.actions.FindActionMap("PlayerBasic");
        attackAction = playerBasicInputMap.FindAction("Attack");
        moveAction = playerBasicInputMap.FindAction("Move");
        animator.SetFloat("ValueY", -1); // Make the player face down by default
    }

    private void Update()
    {
        direction = moveAction.ReadValue<Vector2>();

        if (direction != Vector2.zero)
        {
            animator.SetFloat("ValueX", direction.x);
            animator.SetFloat("ValueY", direction.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnEnable()
    {
        attackAction.performed += OnAttackPerformed;
        attackAction.canceled += OnAttackCanceled;
        playerBasicInputMap.Enable();
    }

    private void OnDisable()
    {
        playerBasicInputMap.Disable();

        attackAction.performed -= OnAttackPerformed;
        attackAction.canceled -= OnAttackCanceled;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Attack performed");
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Attack canceled");
    }

}
