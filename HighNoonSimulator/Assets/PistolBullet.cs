using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    [SerializeField]
    [Range(15,100)]
    public float speed = 45f;

    private void Start()
    {
        //float randx = Random.Range(-0.9f, 0.9f);
        //float randy = Random.Range(-0.9f, 0.9f);
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(0, 0, 0);
        //rb.velocity += transform.forward * speed;
        rb.inertiaTensor = new Vector3(1, 1, 1);
        Invoke("DeSpawn", 1.0f);
    }
    private void Update()
    {
        if(speed!= 0)
            {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }
    //Will need to be changed to raycast
    private void OnCollisionEnter(Collision collision)
    {

        //collision.transform.SendMessage("TakeDamage", 5, SendMessageOptions.DontRequireReceiver);

        

    }
    public void DeSpawn()
    {
        Destroy(gameObject);
    }
}
