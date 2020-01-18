using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class RevolverDamage : MonoBehaviourPunCallbacks
{

    //This script uses raycasting to detect and damage objects
    //public ParticleSystem muzzleFlash;
    public int Damage = 5;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    private float nextFire;
    public int ammo = 6;


    public RaycastHit Shot;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    private void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && ammo > 0)
        {
            Shoot();
            nextFire = Time.time + fireRate;
            ammo --;

        }
    }


    void Shoot()
    {
        RaycastHit Hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit))
        {
            Debug.Log(Hit.transform.name);
            //Hit.transform.SendMessage("DeductPoints", Damage, SendMessageOptions.DontRequireReceiver);
            Hit.collider.gameObject.GetComponent<PhotonView>().RPC("DeductPoints", RpcTarget.AllBuffered, 10f);
        }
    }
}
