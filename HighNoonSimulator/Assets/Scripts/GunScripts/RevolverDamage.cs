using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;

public class RevolverDamage : MonoBehaviourPunCallbacks
{
    public GameObject PlayerUI;
    private int kills = 0;
    //This script uses raycasting to detect and damage objects
    public int Damage = 5;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    private float nextFire;
    private int ammo = 6;
    private bool Fired;
    public float x;
    private bool First = true;

    public RaycastHit Shot;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    private void Start()
    {
       

        Fired = false;
        fpsCam = GetComponentInParent<Camera>();
    }



    // Update is called once per frame
    void Update()
    {
        PlayerUI.transform.Find("Score").GetComponent<Text>().text = "Score: " + kills;
        PlayerUI.transform.Find("Ammo").GetComponent<Text>().text = "6 -" + ammo;
        if (Input.GetButtonDown("Fire1") && Fired == false && ammo > 0)
        {
            StartCoroutine(Fire());
            StopCoroutine(Fire());
            
            var gunSound = GetComponent<AudioSource>();
           gunSound.Play();
           GetComponent<Animation>().Play("Revolver fire");
            Shoot();
            //  nextFire = Time.time + fireRate;
            Fired = true;
            
            
           
            
        }
    }


    IEnumerator Fire()
    {
        
        ammo = ammo - 1;
        Fired = true;
        yield return new WaitForSeconds(0.5f);
        Fired = false;
        
    }
    public void reload()
    {
        ammo = 6;
        Debug.Log("Ammo: " + ammo);
    }
    void Shoot()
    {
        PlayerUI.transform.Find("Score").GetComponent<Text>().text = "Score: " + kills;
        RaycastHit Hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit))
        {
            x = Hit.distance;
            Debug.Log(Hit.transform.name);
            if(Hit.transform.tag == "Enemy" )
            {
                if(First == true)
                {
                    kills++;
                }
                    
                
                if (x <= 40 && x < 30)
                {

                    Hit.collider.gameObject.GetComponent<PhotonView>().RPC("DeductPoints", RpcTarget.AllBuffered, 2f);
                    kills += 3;
                }
                else if ( x <= 30 && x >20)
                {
                    Hit.collider.gameObject.GetComponent<PhotonView>().RPC("DeductPoints", RpcTarget.AllBuffered, 3f);
                    kills += 2;
                }
                else if (x > 20)
                {
                    kills += 1;
                    Hit.collider.gameObject.GetComponent<PhotonView>().RPC("DeductPoints", RpcTarget.AllBuffered, 5f);
                }

                    
            }
            
            First = false;

        }
    }
}
