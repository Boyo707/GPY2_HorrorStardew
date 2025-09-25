using UnityEngine;

public class CameraBounce : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float amplitude = 0.5f;  // How high the camera bounces
    public float frequency = 1f;    // How fast the camera bounces

    private Vector3 startPos;

    void Start()
    {
        // Store the original position of the camera
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate a new Y position using a sine wave
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new position
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
