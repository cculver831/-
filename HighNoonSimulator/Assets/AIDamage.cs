using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamage : MonoBehaviour
{
    private Animator PlayerAnim;
    public float enemyHealth = 10f;


    private void Start()
    {

        PlayerAnim = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
       
        if (enemyHealth <= 0)
        {
            death();
            GameEvents.current.death();
        }
    }
    public void death()
    {
        Debug.Log("Ouch");
        PlayerAnim.SetBool("Idle", false);
        PlayerAnim.SetBool("Dying", true);

    }


    public void Heal()
    {
        enemyHealth = 10;
    }

    public void DeductPoints(int Damage)
    {
        enemyHealth -= Damage;
    }
}