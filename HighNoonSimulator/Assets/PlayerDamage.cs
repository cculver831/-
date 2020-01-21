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
    public Canvas PlayerUI;


    private void Start()
    {
        DeathCam.gameObject.SetActive(false);
        PlayerAnim = gameObject.GetComponent<Animator>();
    }
    [PunRPC]
    void Update()
    {
       if(enemyHealth <= 0)
        {
           
            PlayerUI.gameObject.SetActive(false);
            PlayerAnim.SetBool("Idle", false);
            PlayerAnim.SetBool("Dying", true);
            Cam.gameObject.SetActive(false);
            DeathCam.gameObject.SetActive(true);

            

        }
    }
    [PunRPC]
    public void DeductPoints(int Damage)
    {
        enemyHealth -= Damage;
    }
  

}
