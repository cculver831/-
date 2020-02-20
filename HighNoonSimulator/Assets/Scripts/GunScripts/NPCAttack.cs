using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : MonoBehaviour
{
    //This script uses raycasting to detect and damage objects\
    private int Damage = 1;
    private bool Fired;
    private float x;

    public RaycastHit Shot;


    public void Shooting()
    {
        Debug.Log("I am shooting");
        var gunSound = GetComponent<AudioSource>();
        gunSound.Play();
        GetComponent<Animation>().Play("Revolver fire");


        RaycastHit Hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out Hit))
        {
            
            x = Hit.distance;
            Debug.Log("Distance: " + x);
            if (Hit.transform.tag == "Player")
            {

                if (x > 30)
                {
                    Damage = 1;
                }
                else if (x <= 30 && x > 20)
                {
                    Damage = 2;
                }
                else if (x <= 20)
                {
                    Damage = 3;

                }
            }
            Hit.transform.SendMessage("DeductPoints", Damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}