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

    public int ammo = 6;


    //Pistol Script
    public GameObject BulletSpawn;
    public float coolDownPeriodInSeconds = 0.5f;
    private float timeStamp;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;
    private Camera fpsCam;
 
    private void Start()
    {
        kills = 0;
        //Score.text = "Score: " + kills;
        fpsCam = GetComponentInParent<Camera>();
        effectToSpawn = vfx[0];
    }



    // Update is called once per frame
    void Update()
    {
        //Ammo.text = ammo + " - 0";
        // Fire();
       Shoot();
    }

    void Fire()
    {


        if (Input.GetButtonDown("Fire1") && timeStamp <= Time.time)
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
        }
        var gunSound = GetComponent<AudioSource>();
            gunSound.Play();
            GetComponent<Animation>().Play("Revolver fire");
            ammo = ammo - 1;
        timeStamp = Time.time + coolDownPeriodInSeconds;

    }

    public void reload()
    {
        ammo = 6;

    }
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && timeStamp <= Time.time)
        {
            RaycastHit Hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit))
            {
                // x = Hit.distance;
                Debug.Log("Gun shot distance: " + Hit.distance);
                //Score.text = "Score: " + kills;
               // GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Score = kills;
               // PlayerPrefs.SetInt("HighScore", kills);
                Hit.transform.SendMessage("TakeDamage", 25, SendMessageOptions.DontRequireReceiver);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Hit.distance, Color.cyan);
                // Projectile
                GameObject vfx;
                vfx = Instantiate(effectToSpawn, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
                timeStamp = Time.time + coolDownPeriodInSeconds;

                //Audio 
                var gunSound = GetComponent<AudioSource>();
                gunSound.Play();
                GetComponent<Animation>().Play("Revolver fire");
            }
        }

    }
}

