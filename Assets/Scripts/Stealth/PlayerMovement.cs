using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rb;

    private Vector3 movementDir = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        movementDir = new Vector3(xInput, 0, yInput).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementDir * speed;
    }
}
