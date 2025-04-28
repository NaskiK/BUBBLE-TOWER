using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject tutorialPanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        tutorialPanel.SetActive(false);
        Time.timeScale = 0f; // Pause game when in menu
    }

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f; // Resume game
    }

    public void OpenTutorial()
    {
        mainMenuPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game! (works only in a built game)");
    }
}
