using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform bubble; // Assign the bubble GameObject here
    public float smoothSpeed = 5f; // Adjust for smoother movement

    void LateUpdate()
    {
        if (bubble != null)
        {
            // Follow the bubble's Y position, keep X and Z the same
            Vector3 targetPosition = new Vector3(transform.position.x, bubble.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
