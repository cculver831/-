using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    
    public static GameEvents current;
    public delegate float IncreaseScoreDelegate();
    public static event IncreaseScoreDelegate IncreaseScore;
    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    public event Action onDeath;
    public void death()
    {
        if(onDeath != null)
           {
            onDeath();
            }
    }

    public event Action onKillShot;
    public void KillShot()
    {
        if (onKillShot != null)
        {
            onKillShot();
        }
    }

}
