using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerDamage>().enemyHealth < 10)
            {
                var pickUpSound = GetComponent<AudioSource>();
                pickUpSound.Play();
                other.GetComponent<PlayerDamage>().Heal();
                this.GetComponent<MeshCollider>().enabled = false;
                this.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(spawnHealth());
            }

        }
    }

    public void pickedUp(GameObject enemy)
    {
        Debug.Log("Enemy Healed");
        enemy.GetComponent<NPCDamage>().enemyHealth = 10;
        this.GetComponent<MeshCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(spawnHealth());
    }
    IEnumerator spawnHealth()
    {
        Debug.Log("Please wait for health");
        yield return new WaitForSeconds(5.0f);
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<MeshCollider>().enabled = true; ;
    }
}
