using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainButtons;
    private static readonly int ClosePause = Animator.StringToHash("ClosePause");

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        mainButtons.SetActive(false);
    }

    public void Continue()
    {
        pauseMenu.GetComponent<Animator>().SetTrigger(ClosePause);
        StartCoroutine(DisablePauseMenu());
    }

    IEnumerator DisablePauseMenu()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        FindObjectOfType<SceneController>().QuitGame();
    }

    public void UnPause()
    {
        Continue();
    }

}