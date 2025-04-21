using UnityEngine;
using System.Collections;

public class FishMover : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public float minPauseTime = 0.5f;
    public float maxPauseTime = 2f;
    public float minMoveTime = 1f;
    public float maxMoveTime = 4f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.right * moveDistance;
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(RandomSwim());
    }

    IEnumerator RandomSwim()
    {
        while (true)
        {
            float moveTime = Random.Range(minMoveTime, maxMoveTime);
            float timer = 0f;

            // Swim phase
            while (timer < moveTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                // Flip sprite
                spriteRenderer.flipX = !movingRight;

                // Switch direction if reached edge
                if (Vector3.Distance(transform.position, targetPos) < 0.01f)
                {
                    movingRight = !movingRight;
                    targetPos = startPos + (movingRight ? Vector3.right : Vector3.left) * moveDistance;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            // Pause phase
            float pauseDuration = Random.Range(minPauseTime, maxPauseTime);
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}
