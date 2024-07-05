using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControls : MonoBehaviour
{
    GatherInput2 input;
    Animator anim;
    public bool attackStarted;

    private void Awake()
    {
        input = GetComponent<GatherInput2>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (input.tryToAttack)
        {
            if (!attackStarted)
            {
                attackStarted = true;
                anim.SetBool("Attack", attackStarted);
            }
            input.tryToAttack = false;
        }
    }

    public void ResetAttack()
    {
        attackStarted = false;
        anim.SetBool("Attack", attackStarted);
    }
}
