using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvokeCSharpEvents : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;
    Vector2 direction;

    PlayerInput playerInput;
    InputActionMap playerBasicInputMap;
    InputAction attackAction;
    InputAction moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerBasicInputMap = playerInput.actions.FindActionMap("PlayerBasic");
        attackAction = playerBasicInputMap.FindAction("Attack");
        moveAction = playerBasicInputMap.FindAction("Move");
    }

    private void Update()
    {
        direction = moveAction.ReadValue<Vector2>();
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
