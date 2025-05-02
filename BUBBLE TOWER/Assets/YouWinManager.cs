using UnityEngine;

public class YouWinManager : MonoBehaviour
{
    public GameObject youWinPanel;
    public GameObject mainMenuPanel;

    public AudioSource backgroundMusic;
    public AudioSource winAudioSource;
    public AudioClip winSound;

    private bool hasPlayedWinSound = false;

    void Update()
    {
        if (youWinPanel != null && youWinPanel.activeSelf && !hasPlayedWinSound)
        {
            if (backgroundMusic != null && backgroundMusic.isPlaying)
                backgroundMusic.Stop();

            if (winAudioSource != null && winSound != null)
                winAudioSource.PlayOneShot(winSound);

            hasPlayedWinSound = true;
        }
    }

    public void ReturnToMainMenu()
    {
        
            youWinPanel.SetActive(false);

        
            mainMenuPanel.SetActive(true);

        hasPlayedWinSound = false; // reset for next win
    }
}
