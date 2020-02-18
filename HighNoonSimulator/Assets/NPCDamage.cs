using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDamage : MonoBehaviour
{
    private Animator player;
    public float enemyHealth = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
           
            GetComponent<CapsuleCollider>().enabled = false;
            player.SetBool("Dying", true);
            player.SetBool("Running", false);
            player.SetBool("Idle", false);
           
            //GameEvents.current.death();
        }
    }
    public void DeductPoints(int Damage)
    {
        enemyHealth -= Damage;
        Debug.Log(enemyHealth);
    }
}
