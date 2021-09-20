using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject howToPlay;

    public void StartGame()
    {
        FindObjectOfType<SceneController>().LoadNextLevel();
    }

    public void LoadOptions()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void LoadHowTo()
    {
        howToPlay.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        FindObjectOfType<SceneController>().QuitGame();
    }

}

