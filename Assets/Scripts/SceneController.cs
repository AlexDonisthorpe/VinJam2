using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int _currentSceneIndex;

    private void Start() 
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1;
        _currentSceneIndex = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_WEBPLAYER
            Application.OpenURL("https://itch.io/");

#else
            Application.Quit();
#endif
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        _currentSceneIndex++;
        SceneManager.LoadScene(_currentSceneIndex);
    }
}
