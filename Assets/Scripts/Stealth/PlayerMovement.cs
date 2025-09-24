using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

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

        if (xInput != 0)
        {
            sprite.flipX = xInput > 0;
        }

        animator.SetBool("IsWalking", xInput != 0 || yInput != 0);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementDir * speed;
    }
}
