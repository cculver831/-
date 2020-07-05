using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastLocation : GAction
{
    private AudioSource audioData;
    private AudioClip[] Files;
    public override bool PrePerform()
    {
        Files = GetComponent<Enemy>().AudioFilesSearching;
        target = GetComponent<Enemy>().lastLocation;
        audioData = GetComponent<AudioSource>();

        audioData.clip = Files[Random.Range(0,Files.Length) ];
        
        //Debug.Log("I'ma pull up on ya, son");
        audioData.Play();
        return true;
    }
    private void Update()
    {
        //target = GetComponent<Enemy>().lastLocation;
        if (beliefs.HasState("SeesPlayer") && running)
        {
            target = GetComponent<Enemy>().lastLocation;
            //finishEarly = true;
            GetComponent<GAgent>().CompleteAction();
            //Debug.Log("I stopped looking for you");

        }


    }
    public override bool PostPerform()
    {
        
        beliefs.RemoveState("JustSawPlayer");
       // Debug.Log("I lost you");


        return true;
    }

}
