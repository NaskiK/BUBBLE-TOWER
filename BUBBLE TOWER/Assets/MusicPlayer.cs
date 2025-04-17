using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        // Only one music player allowed
        if (GameObject.FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
