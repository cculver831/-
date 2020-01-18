using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class MenuManager : MonoBehaviour
{
    public static bool PlayerSelect = false;
    public GameObject PlayerS;
    public GameObject optionsUI;
    public GameObject Lobby;
    public GameObject Main_Menu;
    public GameObject RoomJoin;
    public GameObject Status;
    public GameObject CreateRoom;
    public GameObject RoomList;
    public GameObject InputFields;
    public static bool MainM = true;
    private Animator Camera;
    public GameObject Cam;
    private void Start()
    {
        Cam = GameObject.Find("Main Camera");
        Camera = Cam.GetComponent<Animator>();
    }
    //Play Main
    public void Play()
    {
        Camera.Play("Camera_Pan");
        StartCoroutine(ChangeMenu());
        StopCoroutine(ChangeMenu());
    }
    // Player Select Change
    public void SelectButton()
    {
        Camera.SetBool("Selected", true);
        StartCoroutine(PlayerSelectButton());
        StopCoroutine(PlayerSelectButton());
        Status.SetActive(true);
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
        Main_Menu.SetActive(false);
        yield return new WaitForSeconds(3);
        PlayerS.SetActive(true);


    }
}
