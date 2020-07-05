using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

//This script accounts for damage the player has taken
public class PlayerDamage : MonoBehaviourPunCallbacks
{
    private Animator PlayerAnim;
    public float enemyHealth = 10f;
    public Camera DeathCam;
    public Camera Cam;
    public Text Health;
    public GameObject levelTrans;
    delegate void Die();

    private void Start()
    {
        Cam.gameObject.SetActive(true);
        DeathCam.gameObject.SetActive(false);
        PlayerAnim = gameObject.GetComponent<Animator>();
        levelTrans = GameObject.FindGameObjectWithTag("Transition");

    }
    [PunRPC]
    void Update()
    {
        Health.text = enemyHealth + "HP";
        if (enemyHealth <= 0)
        {
            // death();
            Debug.Log("I'm dead :/ ");
        }
    }
    public void death()
    {
        //Changes the animations of the player (There's gotta be a better way to do this)
        PlayerAnim.SetBool("Idle", false);
        PlayerAnim.SetBool("Running", false);
        PlayerAnim.SetBool("Dying", true);
        Cam.gameObject.SetActive(false);
        DeathCam.gameObject.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(Transition());
        StopCoroutine(Transition());
        levelTrans.GetComponent<LevelTransition>().FadeToLevel();
        SceneManager.LoadScene("GameOVer");
        Debug.Log("Changing Level");
        



    }
    IEnumerator Transition()
    {
        yield return new WaitForSeconds(2);
    }

    public void Heal()
    {
        enemyHealth = 10;
    }
    [PunRPC]
    public void DeductPoints(int Damage)
    {
        enemyHealth -= Damage;
    }
  

}
