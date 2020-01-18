using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;
    public GameObject Spawn4;
    public GameObject Player_n;
    public int spawnPoints = 4;
    public Vector3 Spawnpoint;

    public void respawn(GameObject player)
    {
        Spawnpoint = Spawn2.transform.position;
        Debug.Log("Changing Spawn location");
        player.transform.position = Spawn2.transform.position;
        int spawnPoint = Random.Range(0, spawnPoints);
        Player_n = player;
       

        //switch (spawnPoint)
        //    {

        //    case 0:
        //        Debug.Log("Changing Spawn location 1");
        //        player.transform.position = Spawn1.transform.position;
        //        player.GetComponent<PlayerDamage>().enemyHealth = 10;

        //        break;
        //    case 1:
        //        Debug.Log("Changing Spawn location 2");
        //        player.transform.position = Spawn2.transform.position;
        //        //Instantiate(player,( Spawn2.transform.position.x, Spawn2.transform.position.y, Spawn2.transform.position.z),);
        //        //player.GetComponent<PlayerDamage>().enemyHealth = 10;
        //        break;
        //    case 2:
        //        Debug.Log("Changing Spawn location 3");
        //        player.transform.position = Spawn3.transform.position;
        //        player.GetComponent<PlayerDamage>().enemyHealth = 10;
        //        break;
        //    case 4:
        //        Debug.Log("changing spawn location 4");
        //        player.transform.position = Spawn4.transform.position;
        //        player.GetComponent<PlayerDamage>().enemyHealth = 10;
        //        break;



        //}   
    }
}
