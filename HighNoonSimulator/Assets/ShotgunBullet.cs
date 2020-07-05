using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    [SerializeField]
    [Range(15,100)]
    public float speed;
    //without force
    //transform.Translate(Vector2.right* Hspeed* Time.deltaTime);
    private void Start()
    {
        float randx = Random.Range(-0.9f, 0.9f);
        float randy = Random.Range(-0.9f, 0.9f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        rb.velocity += transform.forward * speed;

        Invoke("DeSpawn", 0.5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SendMessage("TakeDamage", 2, SendMessageOptions.DontRequireReceiver);
        //Destroy(gameObject);
    }
    public void DeSpawn()
    {
        Destroy(gameObject);
    }
}
