using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameManager Manager;
    public GameObject Tutorial;
    public static bool PlayerSelect = false;
    public GameObject PlayerS;
    public GameObject GameMode;
    public GameObject optionsUI;
    public GameObject Lobby;
    public GameObject Main_Menu;
    public GameObject RoomJoin;
    public GameObject Status;
    public GameObject CreateRoom;
    public GameObject RoomList;
    public GameObject InputFields;
    public GameObject LevelTransitions;
    public static bool MainM = true;
    public bool Offline;
    private Animator Camera;
    public GameObject Cam;
    private void Start()
    {
        //Status.SetActive(true);
        Cam = GameObject.Find("Main Camera");
        Camera = Cam.GetComponent<Animator>();
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
        Debug.Log("Game Mansger reports: " + Manager.Offline);
        Debug.Log("Menu  reports: " + Offline);
    }
    //Play Main
    public void Play()
    {
        GameMode.SetActive(true);
        Main_Menu.SetActive(false);
    }
    public void PlayOffline()
    {
        PhotonNetwork.Disconnect();
        Manager.Offline = true;
        Debug.Log("Game Mansger reports: " + Manager.Offline);
        Offline = true;
        Camera.Play("Camera_Pan");
        StartCoroutine(ChangeMenu());
        StopCoroutine(ChangeMenu());
    }
    public void PlayOnline()
    {
        Manager.Offline = false;
        Offline = false;
        Camera.Play("Camera_Pan");
        StartCoroutine(ChangeMenu());
        StopCoroutine(ChangeMenu());
    }
    // Player Select Change
    public void SelectButton()
    {
       
        if(!Manager.Offline)
        {
            Status.SetActive(true);
            Camera.SetBool("Selected", true);
            StartCoroutine(PlayerSelectButton());
            StopCoroutine(PlayerSelectButton());
        }
        else
        {
            PlayerS.SetActive(false);
            LevelTransitions.GetComponent<LevelTransition>().FadeToLevel();
          
        }
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BacktoJoinOptions()
    {
        RoomJoin.SetActive(true);
        RoomList.SetActive(false);
    }
    public void Room()
    {
        RoomJoin.SetActive(false);
        CreateRoom.SetActive(true);
        InputFields.SetActive(true);
    }
    //Enter Player Name
    public void EnterName()
    {
        Lobby.SetActive(false);
        RoomJoin.SetActive(true);
    }
    //Back to JoinOptions
    public void BacktoOptions()
    {
        RoomJoin.SetActive(true);
        RoomList.SetActive(false);
    }
    public void JoinRooms()
    {
        RoomJoin.SetActive(false);
        RoomList.SetActive(true);
    }
    //Player Select Animation 
    IEnumerator PlayerSelectButton()
    {
        PlayerS.SetActive(false);
        
        yield return new WaitForSeconds(3);
        Lobby.SetActive(true);

    }
    //Change from Main to Player Select
    IEnumerator ChangeMenu()
    {
     //   Main_Menu.SetActive(false);
        GameMode.SetActive(false);
        yield return new WaitForSeconds(3);
        PlayerS.SetActive(true);


    }
    // Start Game Level Fade
    public void StartGame()
    {
        CreateRoom.SetActive(false);
        Status.SetActive(false);
    }

    //Fade Sequence for Tutorial

    public void FadeStart()
    {
        StartCoroutine(TutorialChange());
        StopCoroutine(TutorialChange());
    }

    IEnumerator TutorialChange()
    {
       Main_Menu.SetActive(false);
       
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Tutoria");
    }

}
