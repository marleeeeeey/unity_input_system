using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    [SerializeField] InputActionAsset actionAsset;

    InputAction jumpAction;
    InputAction moveAction;
    InputAction attackAction;
    InputActionMap playerNormalMap;

    public float valueX;
    public float valueY;
    public bool tryToAttack;
    public bool tryToJump;


    private void Awake()
    {
        playerNormalMap = actionAsset.FindActionMap("PlayerNormal");
        jumpAction = playerNormalMap.FindAction("Jump");
        moveAction = playerNormalMap.FindAction("MoveHorizontal");
        attackAction = playerNormalMap.FindAction("Attack");
    }

    private void OnEnable()
    {
        jumpAction.performed += JumpExample;
        jumpAction.canceled += JumpStopExample;
        attackAction.performed += AttackExample;
        attackAction.canceled += AttackStopExample;

        actionAsset.Enable(); // Enable the entire asset

        // playerNormalMap.Enable(); // Enable individual maps
        // jumpAction.Enable(); // Enable individual actions
    }

    private void OnDisable()
    {
        actionAsset.Disable();

        jumpAction.performed -= JumpExample;
        jumpAction.canceled -= JumpStopExample;
        attackAction.performed -= AttackExample;
        attackAction.canceled -= AttackStopExample;
    }

    private void Update()
    {
        valueX = moveAction.ReadValue<float>();
        Debug.Log("Value X: " + valueX);
    }

    private void JumpExample(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
        // tryToJump = context.ReadValueAsButton();
        tryToJump = true;
    }


    private void JumpStopExample(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping Stopped");
        tryToJump = false;
    }

    public void AttackExample(InputAction.CallbackContext context)
    {
        Debug.Log("Attacking");
        tryToAttack = true;
    }

    public void AttackStopExample(InputAction.CallbackContext context)
    {
        Debug.Log("Attacking Stopped");
        tryToAttack = false;
    }
}
