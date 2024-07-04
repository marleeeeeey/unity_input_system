using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectSintax : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            Debug.Log("W is pressed");
        }
        if (Keyboard.current.aKey.isPressed)
        {
            Debug.Log("A is pressed");
        }
        if (Keyboard.current.sKey.isPressed)
        {
            Debug.Log("S is pressed");
        }
        if (Keyboard.current.dKey.isPressed)
        {
            Debug.Log("D is pressed");
        }
    }
}
