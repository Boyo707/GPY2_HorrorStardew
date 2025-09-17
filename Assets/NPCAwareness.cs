using UnityEngine;

public class NPCAwareness : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionDistance;
    [SerializeField, Range(0,12)] private float detectionRadius;
    [SerializeField] private LayerMask detectionLayer;

    [Header("Suspicion Settings")]
    [SerializeField] private float maxSuspicionLevel;
    [SerializeField] private float suspicionIncreaseSpeed;
    [SerializeField] private float suspicionDecreaseSpeed;
    [SerializeField] private bool isAlerted;

    private bool isInView = false;
    private Vector3 facingDirection;

    [SerializeField]private float currentSuspicion;

    private Transform target;

    private Vector3[] radiusVectors = new Vector3[2];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Normalizing these vectors so the vectors will be easier to compare with the players direction.
        radiusVectors[0] = (new Vector3(detectionRadius, 0, 1) * detectionDistance).normalized;
        radiusVectors[1] = (new Vector3(-detectionRadius, 0, 1) * detectionDistance).normalized;
    }

    // Update is called once per frame
    void Update()
    {

        //general area of the Npc noticing
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionDistance, detectionLayer);

        //if player is inside the area
        if (hitColliders.Length != 0)
        { 
            target = hitColliders[0].transform;
            Vector3 playerDir = (hitColliders[0].transform.position - transform.position).normalized;

            //checking if the player is inside of the area radius aswell
            if (radiusVectors[0].x > playerDir.x && radiusVectors[1].x < playerDir.x 
                && playerDir.z < detectionDistance && transform.position.z < target.position.z)
            {
                isInView = true;
            }
            else
            {
                isInView = false;
            }
        }
        else
        {
            isInView = false;
        }
        Quaternion lol = Quaternion.AngleAxis(-45, Vector3.up);

        ChangeSuspicion(isInView);
    }

    private void ChangeSuspicion(bool increase)
    {
        if (!isAlerted)
        {
            if (increase)
            {
                currentSuspicion += suspicionIncreaseSpeed * Time.deltaTime;
            }
            else
            {
                currentSuspicion -= suspicionDecreaseSpeed * Time.deltaTime;
            }

            currentSuspicion = Mathf.Clamp(currentSuspicion, 0, maxSuspicionLevel);

            if(currentSuspicion >= maxSuspicionLevel) isAlerted = true;
        }
        else
        {
            //AAAAAa
        }
    }

    private void SetAngles()
    {
        //Quaternion direction = Quaternion.AngleAxis(, Vector3.up);
        //radiusVectors[0] = 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
        Gizmos.DrawRay(transform.position, new Vector3(detectionRadius, 0, 1) * detectionDistance);
        //Gizmos.DrawRay(transform.position, new Vector3(-noticeRadius, 0, 1) * noticeDistance);
        Gizmos.DrawRay(transform.position, new Vector3(-detectionRadius, 0, 1) * detectionDistance);
        Gizmos.DrawRay(transform.position, Vector3.forward * detectionDistance);
        Gizmos.DrawRay(transform.position, (target.transform.position - transform.position) );

    }
}
