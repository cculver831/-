using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreTest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDeath += death;
        GameEvents.current.onKillShot += KillShot;
    }

    private void KillShot()
    {
        throw new NotImplementedException();
    }

    private void death()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
