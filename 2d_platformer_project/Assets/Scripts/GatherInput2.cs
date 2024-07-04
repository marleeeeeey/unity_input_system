using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput2 : MonoBehaviour
{
    GameActions controls; // Generated class from the input system

    public float valueX;
    public float valueY;
    public bool tryToAttack;
    public bool tryToJump;

    private void Awake()
    {
        controls = new GameActions();
    }

    private void OnEnable()
    {
        controls.PlayerNormal.Jump.performed += JumpExample;
        controls.PlayerNormal.Jump.canceled += JumpStopExample;
        controls.PlayerNormal.Attack.performed += AttackExample;
        controls.PlayerNormal.Attack.canceled += AttackStopExample;

        controls.PlayerNormal.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerNormal.Disable();

        controls.PlayerNormal.Jump.performed -= JumpExample;
        controls.PlayerNormal.Jump.canceled -= JumpStopExample;
        controls.PlayerNormal.Attack.performed -= AttackExample;
        controls.PlayerNormal.Attack.canceled -= AttackStopExample;
    }

    private void Update()
    {
        valueX = controls.PlayerNormal.MoveHorizontal.ReadValue<float>();
        Debug.Log("Value X: " + valueX);
    }

    private void JumpExample(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
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
