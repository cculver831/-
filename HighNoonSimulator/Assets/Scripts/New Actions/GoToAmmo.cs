using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToAmmo : GAction
{
    
    public override bool PrePerform()
    {

        // Get a free toilet
        target = GWorld.Instance.GetQueue("Ammo").RemoveResource();

        //Debug.Log("Cover Found");
        // Check we got a toilet
        if (target == null)
        {
            Debug.Log("This bitch empty");
            return false;

        }
        
        // Add it to the inventory
        inventory.AddItem(target);
        // Remove it's availability from the world
        GWorld.Instance.GetWorld().ModifyState("FreeAmmo", -1);
        return true;
    }

    public override bool PostPerform()
    {

        // Return the toilet to the pool
        GWorld.Instance.GetQueue("Ammo").AddResource(target);
        // Remove the toilet from the list
        inventory.RemoveItem(target);
        // Give the toilet back to the world
        GWorld.Instance.GetWorld().ModifyState("FreeAmmo", 1);
        //beliefs.RemoveState("safe");
        beliefs.RemoveState("HasNoAmmo");
        beliefs.AddStateOnce("HasAmmo", 0);
        GetComponent<ShootAtPlayer>().Ammo = 20;
        return true;
    }
}
