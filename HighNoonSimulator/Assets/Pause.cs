using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool paused = false;
    public GameObject PauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Stop();
            }
        }


    }

void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
    }
    void Stop()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        paused = true;
    }
}
