using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int currentScore = 0;

    public void updateScore(int points)
    {
        points = Mathf.Clamp(points, 0, Int32.MaxValue);
        currentScore += points;
        Debug.Log("Score:" + currentScore);
    }

    public int getScore()
    {
        return currentScore;
    }
}
