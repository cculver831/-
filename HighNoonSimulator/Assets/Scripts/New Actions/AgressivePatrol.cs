using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressivePatrol : GAction
{
   
    public override bool PrePerform()
    {

        return true;
    }
        private void Update()
        {
           if((beliefs.HasState("SeesPlayer") && running) || (beliefs.HasState("JustSawPlayer") && running))
        {
            GetComponent<GAgent>().CompleteAction();
            //Debug.Log("Stopping from Agressive Patrol");
        }
 
        }

    public override bool PostPerform()
    {
        LookForPlayer();

        //Debug.Log("Agressive Patrol Active");
        return true;


        //return true;
    }
    void LookForPlayer()
    {

    }

  
}
