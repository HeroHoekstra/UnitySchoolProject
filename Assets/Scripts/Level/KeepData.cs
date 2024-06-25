using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keep Data", menuName = "Keep Data")]
public class KeepData : ScriptableObject
{
    // Updates the high score
    // Though the high score counter is broken...
    public int highScore;

    public void UpdateHighScore(int score)
    {
        if (highScore < score)
        {
            highScore = score;
        }
    }
}
