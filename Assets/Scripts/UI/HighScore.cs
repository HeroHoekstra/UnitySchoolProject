using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private GameManager gameManager;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        gameManager = GameObject.Find("MainManager").GetComponent<GameManager>();

        // This is really ugly and doesn't work...
        int hs = gameManager.keepData.highScore;
        if (hs < 10)
        {
            highScoreText.text = "High Score:\n000" + hs;
        } 
        else if (hs < 100)
        {
            highScoreText.text = "High Score:\n00" + hs;
        }
        else if (hs < 1000)
        {
            highScoreText.text = "High Score:\n0" + hs;
        } 
        else
        {
            highScoreText.text = "High Score:\n" + hs;
        }

        int s = gameManager.oldScore;
        if (s < 10)
        {
            highScoreText.text = "High Score:\n000" + s;
        }
        else if (s < 100)
        {
            highScoreText.text = "High Score:\n00" + s;
        }
        else if (s < 1000)
        {
            highScoreText.text = "High Score:\n0" + s;
        }
        else
        {
            highScoreText.text = "High Score:\n" + s;
        }
    }
}
