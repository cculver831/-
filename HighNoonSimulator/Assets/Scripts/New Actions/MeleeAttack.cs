using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : GAction
{
   

    public override bool PrePerform()
    {
     
        return true;
    }

    public override bool PostPerform()
    {
        Debug.Log("PUNCH!");
        return true;
    }
}
