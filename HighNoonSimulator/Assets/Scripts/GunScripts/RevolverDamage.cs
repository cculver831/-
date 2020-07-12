using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;

public class RevolverDamage : MonoBehaviourPunCallbacks
{
    public Text Score;
    public Text Ammo;
   
    public int kills = 0;
    //This script uses raycasting to detect and damage objects
    public int Damage = 5;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    private float nextFire;
    public int ammo = 6;
    private bool Fired;
    public float x;

    //Pistol Script
    //public GameObject bullet;
    public GameObject BulletSpawn;
    public float coolDownPeriodInSeconds = 0.5f;
    private float timeStamp;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;

    public RaycastHit Shot;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    private void Start()
    {
        kills = 0;
        Score.text = "Score: " + kills;
        fpsCam = GetComponentInParent<Camera>();
        effectToSpawn = vfx[0];
    }



    // Update is called once per frame
    void Update()
    {
        Ammo.text = ammo + " - 0";
        Fire();

    }

    void Fire()
    {

        if (Input.GetButtonDown("Fire1") && timeStamp <= Time.time && ammo >0)
        {
            var direction = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.cyan);
                // - send damage to object we hit - \\
                hit.collider.SendMessageUpwards("TakeDamage", 15, SendMessageOptions.DontRequireReceiver);
            }
            GameObject vfx;
            vfx = Instantiate(effectToSpawn, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
            timeStamp = Time.time + coolDownPeriodInSeconds;
            var gunSound = GetComponent<AudioSource>();
            gunSound.Play();
            GetComponent<Animation>().Play("Revolver fire");
            ammo = ammo - 1;
        }
     
    }

    public void reload()
    {
        ammo = 6;

    }
    //void Shoot()
    //{
        
    //    RaycastHit Hit;
    //    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit))
    //    {
    //        x = Hit.distance;
    //        Debug.Log("Gun shot distance: " + Hit.distance);
    //        if(Hit.transform.tag == "Enemy" )
    //        {
 
    //            if (x > 20 )
    //            {
    //                Damage = 3;

    //                kills += 5;
    //            }
    //            else if ( x <= 20 && x > 10)
    //            {
    //                Damage = 4;

    //                kills += 2;
    //            }
    //            else if (x <= 10)
    //            {
    //                Damage = 5;
                 
    //                kills += 1;
    //            }
    //        }
    //        Score.text = "Score: " + kills;
    //        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Score = kills;
    //        PlayerPrefs.SetInt("HighScore", kills);
    //        Hit.transform.SendMessage("DeductPoints", Damage, SendMessageOptions.DontRequireReceiver);
    //    }
    //}
}
