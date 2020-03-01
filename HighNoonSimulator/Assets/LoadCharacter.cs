using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCharacter : MonoBehaviour
{
    private GameObject GameManager;
    private GameObject player;
    private GameObject UI;
    private Text Score;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        player = GameManager.GetComponent<GameManager>().Player;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<PlayerLook>().enabled = false;
        player.GetComponentInChildren<PlayerDamage>().DeathCam.enabled = false;
        player.GetComponentInChildren<PlayerDamage>().Cam.enabled = false;
        UI = GameObject.Find("PlayerUI");
        UI.SetActive(false);
        Instantiate(player,transform.position, transform.rotation);
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
