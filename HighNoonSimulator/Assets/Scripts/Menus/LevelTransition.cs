using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public Animator animator;

    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
        
    }
    public void fadeComplete()
    {
        SceneManager.LoadScene("Main");
    }
}
