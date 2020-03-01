using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetScore : MonoBehaviour
{
    public int Score;
    public Text ScoreText;
    private GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        Score = GameManager.GetComponent<GameManager>().Score;
        ScoreText.text = "Score: " + Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
