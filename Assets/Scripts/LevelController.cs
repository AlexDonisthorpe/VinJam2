using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int currentGhosts = 0;
    public int totalHauntings = 0;
    public int remainingChances = 3;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void UpdateCurrentGhosts()
    {
        currentGhosts += 1;
    }

    public void UpdateTotalHauntings()
    {
        totalHauntings += 1;
    }

    public void UpdateChances()
    {
        remainingChances--;
        if (remainingChances <= 0)
        {
            EndGame(false);
        }
    }

    public void EndGame(bool wonGame)
    {
        Time.timeScale = 0;
    }
}
