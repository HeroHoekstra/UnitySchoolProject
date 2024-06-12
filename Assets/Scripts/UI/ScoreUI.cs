using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public int score;

    private GameManager gameManager;
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        gameManager = GameObject.Find("MainManager").GetComponent<GameManager>();
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (score < 10)
        {
            scoreText.text = "Score: 000" + score;
        } 
        else if (score < 100)
        {
            scoreText.text = "Score: 00" + score;
        }
        else if (score < 1000)
        {
            scoreText.text = "Score: 0" + score;
        }
        else
        {
            scoreText.text = "Score: " + score;
        }

        gameManager.score = score;
    }
}
