using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : GAction
{
   

    public override bool PrePerform()
    {
        GetComponent<Animator>().SetBool("Melee", true);
        return true;
    }

    public override bool PostPerform()
    {
        Debug.Log("PUNCH!");
        GetComponent<Animator>().SetBool("Melee", false);
        return true;
    }
}
