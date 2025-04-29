using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public AudioSource gameMusic;
    public AudioSource menuMusic;

    public void PlayGameMusic()
    {
        if (menuMusic.isPlaying) menuMusic.Stop();
        if (!gameMusic.isPlaying) gameMusic.Play();
    }

    public void PlayMenuMusic()
    {
        if (gameMusic.isPlaying) gameMusic.Stop();
        if (!menuMusic.isPlaying) menuMusic.Play();
    }
}
