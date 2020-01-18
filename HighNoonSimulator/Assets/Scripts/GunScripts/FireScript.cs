using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var gunSound  = GetComponent<AudioSource>();
            gunSound.Play();
            GetComponent<Animation>().Play("Revolver fire");
        }
    }
}
