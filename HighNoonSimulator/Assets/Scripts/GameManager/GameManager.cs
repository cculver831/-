using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public MenuManager _MenuManager;
    public GameObject[] PlayerModels;
    public static int count;
    private bool done;
    public bool Offline;
    public AudioSource AudioManager;
    public GameObject[] Spawnloc = new GameObject[4];

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
        Debug.Log("Done: " + done);
        Debug.Log("Gamemanager Offline: " + Offline);
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (Offline == true)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main") && done == false)
            {
                
                Instantiate(PlayerModels[count], new Vector3(2, 2f, 47), Quaternion.Euler(0, 180, 0));
                done = true;

            }
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Check if player is connected to Photon and Scene has just loaded in
        if (PhotonNetwork.IsConnectedAndReady && done == false)
        {
            int newpoint = Random.Range(-6, 6);
            PhotonNetwork.Instantiate(PlayerModels[count].name, new Vector3(newpoint, 0f, newpoint), Quaternion.identity);
            done = true;
            Debug.Log("Player Created");
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
