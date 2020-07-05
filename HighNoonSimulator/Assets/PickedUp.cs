using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour
{
    public static int test;
    private void Update()
    {

      
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("It's touching me!!!)");
            GetComponent<BoxCollider>().isTrigger = true;

            //We eventually want to change destroy object to adding it to collision.addtoinventory();
            //Destroy(gameObject, 5);

        }
      
    }

}
