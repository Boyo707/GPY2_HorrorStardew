using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 distanceFromTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanceFromTarget = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = distanceFromTarget + target.position;
    }
}
