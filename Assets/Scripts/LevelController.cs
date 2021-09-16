using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int currentGhosts = 0;
    public int totalHauntings = 0;

    public void UpdateCurrentGhosts()
    {
        currentGhosts += 1;
    }

    public void UpdateTotalHauntings()
    {
        totalHauntings += 1;
    }
}
