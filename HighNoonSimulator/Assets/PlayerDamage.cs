using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class PlayerDamage : MonoBehaviourPunCallbacks
{
    private Animator PlayerAnim;
    public float enemyHealth = 10f;
    public Camera DeathCam;
    public Camera Cam;
    public Text Health;
    delegate void Die();

    private void Start()
    {
        Cam.gameObject.SetActive(true);
        DeathCam.gameObject.SetActive(false);
        PlayerAnim = gameObject.GetComponent<Animator>();

    }
    [PunRPC]
    void Update()
    {
        Health.text = enemyHealth + "HP";
        if (enemyHealth <= 0)
        {
            death();
            GameEvents.current.death();
        }
    }
    public void death()
    {
        
        PlayerAnim.SetBool("Idle", false);
        PlayerAnim.SetBool("Dying", true);
        Cam.gameObject.SetActive(false);
        DeathCam.gameObject.SetActive(true);

    }
    

    public void Heal()
    {
        enemyHealth = 10;
    }
    [PunRPC]
    public void DeductPoints(int Damage)
    {
        enemyHealth -= Damage;
    }
  

}
