using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BubbleController : MonoBehaviour
{
    public float moveSpeed = 1.1f;
    public float verticalSpeed = 1.1f;
    public float acceleration = 5f;
    public float gravity = 0.7f;

    private Vector2 velocity;
    private Rigidbody2D rb;
    private Vector3 respawnPosition;

    public GameObject youPoppedUI;
    public AudioSource bubbleAudioSource;
    public AudioClip popSound;

    public Image fadeImage;
    public GameObject youWonUI;
    public float floatUpSpeed = 2f;

    private bool isPopped = false;
    private bool isWinning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        velocity = Vector2.zero;
        respawnPosition = transform.position;

        if (bubbleAudioSource == null)
            bubbleAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPopped)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Retry();
            }
            return;
        }

        if (isWinning)
        {
            rb.linearVelocity = new Vector2(0, floatUpSpeed);
            return;
        }

        Vector2 targetVelocity = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) targetVelocity.y += verticalSpeed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) targetVelocity.y -= verticalSpeed;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) targetVelocity.x -= moveSpeed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) targetVelocity.x += moveSpeed;

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow))
        {
            targetVelocity.y -= gravity;
        }

        velocity = Vector2.Lerp(velocity, targetVelocity, Time.deltaTime * acceleration);
        rb.linearVelocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Pop();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawnPosition = other.transform.position;
            Debug.Log("Checkpoint reached!");
        }
        else if (other.CompareTag("Win"))
        {
            StartCoroutine(WinSequence());
        }
    }

    void Pop()
    {
        if (isPopped) return;

        isPopped = true;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        if (bubbleAudioSource && popSound)
            bubbleAudioSource.PlayOneShot(popSound);

        GameManager.Instance?.StopTimer();

        if (youPoppedUI != null)
            youPoppedUI.SetActive(true);
        else
            Respawn();
    }

    public void Retry()
    {
        if (youPoppedUI != null)
            youPoppedUI.SetActive(false);

        Respawn();
        GameManager.Instance?.AddAttempt();
    }

    void Respawn()
    {
        transform.position = respawnPosition;
        rb.isKinematic = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        velocity = Vector2.zero;
        isPopped = false;
    }

    public void ResetToStart()
    {
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = false;
        isPopped = false;
        transform.position = respawnPosition;
    }

    IEnumerator WinSequence()
    {
        isWinning = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        Time.timeScale = 1f;

        float t = 0;
        Color c = fadeImage.color;
        while (t < 1f)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t);
            fadeImage.color = c;
            yield return null;
        }

        youWonUI.SetActive(true);
        isWinning = false;
    }
}
