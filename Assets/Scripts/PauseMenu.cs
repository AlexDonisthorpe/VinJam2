using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainButtons;
    private static readonly int ClosePause = Animator.StringToHash("ClosePause");

    public void Pause()
    {
        FindObjectOfType<AudioController>().TogglePauseMusic();
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
        FindObjectOfType<Controller>().Unpause();
        StartCoroutine(DisablePauseMenu());
    }

    IEnumerator DisablePauseMenu()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        FindObjectOfType<AudioController>().TogglePauseMusic();
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
