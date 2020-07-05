using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    //public GameObject bullet;
    public GameObject BulletSpawn;
    public float coolDownPeriodInSeconds = 0.5f;
    private float timeStamp;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;
    private void Start()
    {
        effectToSpawn = vfx[0];
    }
    // Update is called once per frame
    void Update()
    {
        Fire();
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
           // timeStamp = Time.time + coolDownPeriodInSeconds;
        }
    }
}
