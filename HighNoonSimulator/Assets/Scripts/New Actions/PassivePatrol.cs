using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivePatrol : GAction
{
   
    public override bool PrePerform()
    {

        return true;
    }

    public override bool PostPerform()
    {


        return true;
    }

    private void Update()
    {
        if (beliefs.HasState("activated"))
        {
            finishEarly = true;

        }

    }

}
