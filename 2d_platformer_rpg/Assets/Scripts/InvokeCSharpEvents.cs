using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvokeCSharpEvents : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject menuObject;

    // Components
    Rigidbody2D rb;
    PlayerInput playerInput;
    Animator animator;

    // Input Maps
    InputActionMap playerBasicInputMap;
    InputActionMap uiInputMap;
    InputActionMap mapSwitcherInputMap;

    // Input Actions
    InputAction attackAction;
    InputAction moveAction;
    InputAction menuActivateDeactivateAction;

    // States
    Vector2 direction;

    private void Awake()
    {
        // Components
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        // Input Maps
        playerBasicInputMap = playerInput.actions.FindActionMap("PlayerBasic");
        uiInputMap = playerInput.actions.FindActionMap("UI");
        mapSwitcherInputMap = playerInput.actions.FindActionMap("MapSwitcher");

        // Input Actions
        attackAction = playerBasicInputMap.FindAction("Attack");
        moveAction = playerBasicInputMap.FindAction("Move");
        menuActivateDeactivateAction = mapSwitcherInputMap.FindAction("Menu");

        // Default animation setup
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

        menuActivateDeactivateAction.performed += MenuControl;

        uiInputMap.Disable();
        playerBasicInputMap.Enable();
        mapSwitcherInputMap.Enable();
    }

    private void OnDisable()
    {
        uiInputMap.Disable();
        playerBasicInputMap.Disable();

        attackAction.performed -= OnAttackPerformed;
        attackAction.canceled -= OnAttackCanceled;

        menuActivateDeactivateAction.performed -= MenuControl;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Attack performed");
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Attack canceled");
    }


    InputActionMap placeholderInputMap;
    private void MenuControl(InputAction.CallbackContext context)
    {
        Debug.Log("Menu activated/deactivated");

        if (menuObject == null)
        {
            Debug.LogError("Menu Canvas is not assigned");
            return;
        }

        if (placeholderInputMap == null)
        {
            placeholderInputMap = playerInput.currentActionMap;
        }

        if (menuObject.activeSelf)
        {
            menuObject.SetActive(false);
            playerInput.SwitchCurrentActionMap(placeholderInputMap.name);
            placeholderInputMap = null;
        }
        else
        {
            menuObject.SetActive(true);
            playerInput.SwitchCurrentActionMap(uiInputMap.name);
        }
    }

}
