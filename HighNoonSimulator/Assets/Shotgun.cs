using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int ShotCount = 12;
    public float spreadAngle;

    List<Quaternion> shots;
    public float coolDownPeriodInSeconds = 0.5f;
    private float timeStamp;
    public GameObject bullet;
    public Transform BulletSpawn;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;

    float variance = 1.0f;
    float distance = 10.0f;
    // Start is called before the first frame update
    void Awake()
    {

        effectToSpawn = vfx[0];
    }

    // Update is called once per frame  && timeStamp <= Time.time
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && timeStamp <= Time.time)
        {
            Fire();

        }
    }
    void Fire()
    {
        GameObject vfx;
       
        for (var i = 0; i < 20; i++)
        {
            Vector3 direction = BulletSpawn.transform.forward; // your initial aim.
            Vector3 spread = new Vector3(0, 0, 0); 
            spread += BulletSpawn.transform.up * Random.Range(-1f, 1f); // add random up or down (because random can get negative too)
            spread += BulletSpawn.transform.right * Random.Range(-1f, 1f); // add random left or right

            // Using random up and right values will lead to a square spray pattern. If we normalize this vector, we'll get the spread direction, but as a circle.
            // Since the radius is always 1 then (after normalization), we need another random call. 
            direction += spread.normalized * Random.Range(0f, 0.2f);
            Vector3 bulletPath = direction + BulletSpawn.transform.position;
            //Debug.Log("direction: " + direction);
            RaycastHit hit;
            //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100)
            if (Physics.Raycast(transform.position,  direction, out hit, 100))
            {

                Debug.DrawLine(BulletSpawn.transform.position, hit.point, Color.green);

                // - send damage to object we hit - \\
                hit.collider.SendMessageUpwards("TakeDamage", 20, SendMessageOptions.DontRequireReceiver);
            }
            vfx = Instantiate(effectToSpawn, bulletPath, BulletSpawn.transform.rotation);

            timeStamp = Time.time + coolDownPeriodInSeconds;
        }
    }
}
