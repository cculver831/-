using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{

    //Setting up array of enemies
    public GameObject Player;
    public GameObject[] Enemies;
    public GameObject[] spawnLoc;
    private int spawnAmount = 0;
    public int total; 
    public float SpawnRate = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }


    //Spawn Enemies on increment
    IEnumerator Spawn()
    {
        Debug.Log("enemies Spawning!");
        while(true)
        {
            int s = Random.Range(0, 3);
            int e = Random.Range(0, 6);
            Instantiate(Enemies[e], new Vector3(spawnLoc[s].transform.position.x, spawnLoc[s].transform.position.y, spawnLoc[s].transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(SpawnRate);
            spawnAmount++;
        }
        SpawnRate = SpawnRate / 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
