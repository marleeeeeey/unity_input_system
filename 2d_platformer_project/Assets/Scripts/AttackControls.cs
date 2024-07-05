using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControls : MonoBehaviour
{
    GatherInput2 input;

    private void Awake()
    {
        input = GetComponent<GatherInput2>();
    }

    void Update()
    {
        if (input.tryToAttack)
        {
            Debug.Log("Attacking");
            input.tryToAttack = false;
        }
    }
}
