using UnityEngine;
using UnityEngine.SceneManagement;
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

    private bool isPopped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // We simulate gravity manually
        rb.freezeRotation = true;

        velocity = Vector2.zero;
        respawnPosition = transform.position;

        if (bubbleAudioSource == null)
            bubbleAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPopped) return;

        Vector2 targetVelocity = Vector2.zero;

        // WASD input
        if (Input.GetKey(KeyCode.W)) targetVelocity.y += verticalSpeed;
        if (Input.GetKey(KeyCode.S)) targetVelocity.y -= verticalSpeed;
        if (Input.GetKey(KeyCode.A)) targetVelocity.x -= moveSpeed;
        if (Input.GetKey(KeyCode.D)) targetVelocity.x += moveSpeed;

        // Apply gravity when not pressing anything upward
        if (!Input.GetKey(KeyCode.W))
        {
            targetVelocity.y -= gravity;
        }

        // Smooth acceleration
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
    }

    void Pop()
    {
        if (isPopped) return;

        isPopped = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;

        if (bubbleAudioSource && popSound)
            bubbleAudioSource.PlayOneShot(popSound);

        GameManager.Instance?.StopTimer();

        if (youPoppedUI != null)
            youPoppedUI.SetActive(true);
        else
            StartCoroutine(RespawnAfterPop());
    }

    public void Retry()
    {
        if (youPoppedUI != null)
            youPoppedUI.SetActive(false);

        StartCoroutine(RespawnAfterPop());
        GameManager.Instance?.AddAttempt();
    }

    IEnumerator RespawnAfterPop()
    {
        // No delay — instant respawn
        transform.position = respawnPosition;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = false;
        velocity = Vector2.zero;

        isPopped = false;

        yield return null; // Optional — lets Unity finish the frame
    }
}
