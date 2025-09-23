using Unity.VisualScripting;
using UnityEngine;

public class NPCWonder : MonoBehaviour
{
    public enum WonderState
    {
        Waiting,
        FindCheckPoint,
        Moving
    }

    [SerializeField] private NPCAwareness awareness;
    [SerializeField] private Vector2 wonderAreaSize;

    [Header("Normal Patrol")]
    [SerializeField] private float normalMoveSpeed;
    [SerializeField] private float normalMoveDelay;

    [Header("Panic Patrol")]
    [SerializeField] private float panicMoveSpeed;
    [SerializeField] private float panicMoveDelay;

    private float currentMoveSpeed;
    private float currentMoveDelay;

    private WonderState currentState = WonderState.Waiting;

    private bool isWondering;
    private float currentTime;

    private Vector3 nextPoint;
    private Vector3 moveDir;

    private Rigidbody rB;

    public Vector3 MoveDirection
    {
        get { return moveDir; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMoveDelay = normalMoveDelay;
        currentMoveSpeed = normalMoveSpeed;

        currentTime = currentMoveDelay;
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (awareness.IsAlerted)
        {
            currentMoveDelay = panicMoveDelay;
            currentMoveSpeed = panicMoveSpeed;
        }

        switch (currentState)
        {
            case WonderState.Waiting:
                if(currentTime > 0)
                {
                    currentTime -= Time.deltaTime;
                }
                else
                {
                    currentState = WonderState.FindCheckPoint;
                }
                break;

            case WonderState.FindCheckPoint:
                currentTime = currentMoveDelay;

                float xDif = wonderAreaSize.x / 2;
                float yDif = wonderAreaSize.y / 2;

                float x = Random.Range(transform.position.x - xDif, transform.position.x + xDif);
                float z = Random.Range(transform.position.z - yDif, transform.position.z + yDif);

                nextPoint = new Vector3(x, transform.position.y, z);

                moveDir = (nextPoint - transform.position).normalized;

                currentState = WonderState.Moving;
                break;

            case WonderState.Moving:
                if(Vector3.Distance(transform.position, nextPoint) < 0.1f)
                {
                    currentState = WonderState.Waiting;
                }
                break;
        }

        if (awareness.IsInSight && !awareness.IsAlerted)
        {
            currentState = WonderState.Waiting;
        }
    }

    private void FixedUpdate()
    {
        if (currentState == WonderState.Moving) rB.linearVelocity = moveDir * currentMoveSpeed;
        else rB.linearVelocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, new Vector3(wonderAreaSize.x, 0, wonderAreaSize.y));

        Gizmos.DrawSphere(nextPoint, 1);
    }
}
