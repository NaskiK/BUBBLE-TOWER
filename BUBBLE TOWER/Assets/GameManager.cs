using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI attemptText;
    public TextMeshProUGUI timerText;

    private int attempts = 1;
    private float timer = 0f;
    private bool isGameOver = false;

    void Awake()
    {
        // Singleton pattern to keep one GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (!isGameOver)
        {
            timer += Time.deltaTime;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (attemptText != null)
            attemptText.text = "Attempt: " + attempts;

        if (timerText != null)
            timerText.text = "Time: " + timer.ToString("F1") + "s";
    }

    public void AddAttempt()
    {
        attempts++;
        timer = 0;
        isGameOver = false;
    }

    public void StopTimer()
    {
        isGameOver = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-link UI after scene reload
        attemptText = GameObject.Find("AttemptText")?.GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }
}
