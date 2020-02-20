using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public GameObject Player;
    public MenuManager _MenuManager;
    public GameObject[] PlayerModels;
    public static int count;
    private bool done;
    public bool Offline;
    public AudioSource AudioManager;
    public GameObject[] Spawnloc = new GameObject[4];
    public int Score;

    void Awake()
    {
        
        //Singleton 
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        _MenuManager.Offline = Offline;
        //Check if arena is loaded in
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (Offline == true)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main") && done == false)
            {
                Player = PlayerModels[count];
                Player.GetComponent<PlayerMovement>().enabled = true;
                Player.GetComponentInChildren<PlayerLook>().enabled = true;
                Player.GetComponentInChildren<PlayerDamage>().DeathCam.enabled = true;
                Player.GetComponentInChildren<PlayerDamage>().Cam.enabled = true;
                Player.GetComponentInChildren<RevolverDamage>().enabled = true;
                Instantiate(PlayerModels[count], new Vector3(2, 2f, 47), Quaternion.Euler(0, 180, 0));
                done = true;
                
               // Score.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
            }
           
        }
       // Score = PlayerModels[count].GetComponentInChildren<RevolverDamage>().kills;
        Debug.Log("Game Manager Score: " + Score);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Check if player is connected to Photon and Scene has just loaded in
        if (PhotonNetwork.IsConnectedAndReady && done == false)
        {
            int newpoint = Random.Range(-6, 6);
            PhotonNetwork.Instantiate(PlayerModels[count].name, new Vector3(newpoint, 0f, newpoint), Quaternion.identity);
            done = true;
        }
    }
    private void FindSpawn()
    {
        for(int i = 0; i < 4; i++)
        {
            Spawnloc[i] = GameObject.FindGameObjectWithTag("Respawn");
            Debug.Log("Spawn Location: " + Spawnloc[i].name + " has been found");
        }
    }
}
