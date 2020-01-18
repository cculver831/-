using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static bool PlayerSelect = false;
    public GameObject PlayerS;
    public GameObject optionsUI;
    public GameObject Lobby;
    public GameObject Main_Menu;
    public GameObject RoomJoin;
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
    }
    //Enter Player Name
    public void EnterName()
    {
        Lobby.SetActive(false);
        RoomJoin.SetActive(true);
    }
    //Player Select Animation 
    IEnumerator PlayerSelectButton()
    {
        PlayerS.SetActive(false);
        yield return new WaitForSeconds(4);
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
