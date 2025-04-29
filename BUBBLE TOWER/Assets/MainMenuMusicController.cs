using UnityEngine;

public class MainMenuMusicController : MonoBehaviour
{
    public GameObject mainMenuPanel;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (mainMenuPanel.activeSelf)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        if (mainMenuPanel != null)
        {
            if (!mainMenuPanel.activeSelf && audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}
