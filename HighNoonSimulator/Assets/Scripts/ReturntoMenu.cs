using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturntoMenu : MonoBehaviour
{
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Start");
    }
}
