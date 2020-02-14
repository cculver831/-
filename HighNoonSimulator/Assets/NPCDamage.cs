using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDamage : MonoBehaviour
{
    private float enemyHealth = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Debug.Log("Dead");
            //GameEvents.current.death();
        }
    }
    public void DeductPoints(int Damage)
    {
        enemyHealth -= Damage;
    }
}
