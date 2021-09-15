using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    private bool countdownStarted = false;
    private static readonly int StartTimer = Animator.StringToHash("StartTimer");

    private void Update()
    {
        if (!countdownStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                countdownStarted = true;
                GetComponent<Animator>().SetTrigger(StartTimer);
                UpdateText("5");
            }
        }
    }


    public void UpdateText(string text)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void StartGame()
    {
        FindObjectOfType<LevelTimer>().StartTimer();
        gameObject.SetActive(false);
    }
}
