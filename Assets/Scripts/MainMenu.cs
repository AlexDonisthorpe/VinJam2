using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;

    public void StartGame()
    {
        FindObjectOfType<SceneController>().LoadNextLevel();
    }

    public void LoadOptions()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        FindObjectOfType<SceneController>().QuitGame();
    }

}

