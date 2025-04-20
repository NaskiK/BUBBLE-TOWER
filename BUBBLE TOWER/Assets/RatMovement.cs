using UnityEngine;
using System.Collections;

public class RatMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;
    public float minPauseTime = 0.5f;
    public float maxPauseTime = 2f;

    private Vector3 startPos;
    private Vector3 leftTarget;
    private Vector3 rightTarget;
    private bool movingRight = true;
    private bool isPaused = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPos = transform.position;
        leftTarget = startPos - Vector3.right * moveDistance;
        rightTarget = startPos + Vector3.right * moveDistance;

        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            if (!isPaused)
            {
                Vector3 target = movingRight ? rightTarget : leftTarget;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

                // Flip the sprite depending on direction
                if (spriteRenderer != null)
                    spriteRenderer.flipX = movingRight;

                if (Vector3.Distance(transform.position, target) < 0.01f)
                {
                    movingRight = !movingRight;
                    isPaused = true;
                    float pauseTime = Random.Range(minPauseTime, maxPauseTime);
                    yield return new WaitForSeconds(pauseTime);
                    isPaused = false;
                }
            }
            yield return null;
        }
    }
}
