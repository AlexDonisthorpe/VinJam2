using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject LoseUI;
    [SerializeField] private GameObject winLoseUI;
    [SerializeField] private TextMeshProUGUI winGhostsNoField;
    [SerializeField] private TextMeshProUGUI ghostCounterText;
    [SerializeField] private Image[] ChanceImages;
    
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
        UpdateGhostUI();
    }

    private void UpdateGhostUI()
    {
        ghostCounterText.text = currentGhosts.ToString();
    }

    public void UpdateTotalHauntings()
    {
        totalHauntings += 1;
    }

    public void UpdateChances()
    {
        remainingChances--;
        UpdateChancesUI();
        if (remainingChances <= 0)
        {
            EndGame(false);
        }
    }

    private void UpdateChancesUI()
    {
        ChanceImages[remainingChances].enabled = false;
    }

    public void EndGame(bool wonGame)
    {
        Time.timeScale = 0;
        winLoseUI.SetActive(true);
        
        if (wonGame)
        {
            winUI.SetActive(true);
            winGhostsNoField.text = currentGhosts.ToString();
        }
        else
        {
            LoseUI.SetActive(false);
        }
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        FindObjectOfType<SceneController>().LoadScene(1);
    }

    public void QuitGame()
    {
        FindObjectOfType<SceneController>().QuitGame();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        FindObjectOfType<SceneController>().LoadScene(0);
    }
}
