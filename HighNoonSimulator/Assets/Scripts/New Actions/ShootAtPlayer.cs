using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : GAction
{
    public GameObject BulletSpawn;
    public GameObject bullet;
    public int Ammo = 20;

    public override bool PrePerform()
    {

        if(Ammo > 0 )
        {
            GetComponent<Animator>().SetBool("Shooting", true);
            return true;
        }
          
        else
        {
            Debug.Log("We outta Ammo");
            beliefs.AddStateOnce("HasNoAmmo", 0);
            beliefs.RemoveState("HasAmmo");
            return false;
        }


    }
    
    public override bool PostPerform()
    {
       
        //beliefs.RemoveState("SeesPlayer");
        //CancelInvoke("Fire");
        GameObject bulletObject = Instantiate(bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        Ammo--;
        Debug.Log("Ammo: " + Ammo);
        GetComponent<Animator>().SetBool("Shooting", false);
        return true;
    }

}
