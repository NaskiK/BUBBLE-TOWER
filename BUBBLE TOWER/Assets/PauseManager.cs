using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject mainMenuPanel;
    public AudioSource musicAudioSource;
    public float normalMusicVolume = 1f;
    public float pausedMusicVolume = 0.3f;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        if (musicAudioSource != null)
            musicAudioSource.volume = normalMusicVolume;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        if (musicAudioSource != null)
            musicAudioSource.volume = pausedMusicVolume;
    }

    public void LoadMainMenu()
    {
        musicAudioSource.volume = normalMusicVolume;
        pauseMenuUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
