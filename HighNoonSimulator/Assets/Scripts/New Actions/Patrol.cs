using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : GAction
{
    
    //public Waypoint wp;
    public override bool PrePerform()
    {

        return true;
    }


    public override bool PostPerform()
    {
        LookForPlayer();

        Debug.Log("Task Performed");
        return true;


        //return true;
    }
    void LookForPlayer()
    {

    }

    //void patrol()
    //{
    //    if (!beliefs.HasState("Doesn'tSeePlayer"))
    //    {
    //        beliefs.ModifyState("activated", 0);
    //        Debug.Log("Agent Activated");
    //        //agent.Stop();
    //        agent.ResetPath();
    //    }
    //    else
    //    {
    //        Debug.Log("We's patrolling, mama");
    //        agent.SetDestination(wp.GetPosition());
    //        if (agent.remainingDistance <= 0f)
    //        {
    //            wp = wp.nextWaypoint;
    //            agent.SetDestination(wp.GetPosition());
    //        }
    //    }

    //}
}
