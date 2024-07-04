using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmbeddedActions : MonoBehaviour
{
    public InputAction jumpAction;
    public InputAction moveAction;

    void Start()
    {
        jumpAction.performed += OnPerformed;
        jumpAction.canceled += OnCanceled;
        jumpAction.started += OnStarted;
        jumpAction.Enable();

        moveAction.performed += OnMove;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.performed -= OnPerformed;
        jumpAction.canceled -= OnCanceled;
        jumpAction.started -= OnStarted;
        jumpAction.Disable();

        moveAction.performed -= OnMove;
        moveAction.Disable();
    }

    private void OnPerformed(InputAction.CallbackContext context)
    {
        bool value = context.ReadValueAsButton();
        Debug.Log("[EmbeddedActions.OnPerformed] " + value);
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        bool value = context.ReadValueAsButton();
        Debug.Log("[EmbeddedActions.OnCanceled] " + value);
    }

    private void OnStarted(InputAction.CallbackContext context)
    {
        bool value = context.ReadValueAsButton();
        Debug.Log("[EmbeddedActions.OnStarted] " + value);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Debug.Log("[EmbeddedActions.OnMove] " + value);
    }

    private void Update()
    {
        // Also possible to read the value from the InputAction without using the callback
        Debug.Log("[EmbeddedActions.Update] Move " + moveAction.ReadValue<Vector2>());
    }

}
