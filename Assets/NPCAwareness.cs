using UnityEngine;
using UnityEngine.UI;

public class NPCAwareness : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionDistance;
    [SerializeField, Range(0,125)] private float detectionRadius;
    [SerializeField] private LayerMask detectionLayer;

    [Header("Suspicion Settings")]
    [SerializeField] private Slider detectionSlider;
    [SerializeField] private GameObject alertedVisual;
    [SerializeField] private float maxSuspicionLevel;
    [SerializeField] private float suspicionIncreaseSpeed;
    [SerializeField] private float suspicionDecreaseSpeed;
    [SerializeField] private bool isAlerted;

    public RaycastHit[] hits;

    private float fovDotProduct;

    [SerializeField]private float currentSuspicion;

    private Transform target;

    void Start()
    {
        //The z axis of the angle determines the value the dot product has to check
        fovDotProduct = (Quaternion.Euler(0, detectionRadius, 0) * transform.forward * detectionDistance).normalized.z;

        detectionSlider.maxValue = maxSuspicionLevel;
    }

    void Update()
    {
        if (!isAlerted)
        {
            if (IsInView())
            {
                currentSuspicion += suspicionIncreaseSpeed * Time.deltaTime;
            }
            else
            {
                currentSuspicion -= suspicionDecreaseSpeed * Time.deltaTime;
            }

            currentSuspicion = Mathf.Clamp(currentSuspicion, 0, maxSuspicionLevel);

            if (currentSuspicion >= maxSuspicionLevel) isAlerted = true;
        }
        else
        {
            alertedVisual.gameObject.SetActive(true);
        }

        detectionSlider.value = currentSuspicion;

        detectionSlider.gameObject.SetActive(detectionSlider.value != 0 && detectionSlider.value != maxSuspicionLevel);
    }



    private bool IsInView()
    {
        //general area of the Npc noticing
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionDistance, detectionLayer);

        //if player is inside the area
        if (hitColliders.Length != 0)
        {
            target = hitColliders[0].transform;
            Vector3 playerDir = (hitColliders[0].transform.position - transform.position).normalized;

            //checking if the player is inside of the area radius aswell
            //using dot product to check the angle
            if (Vector3.Dot(playerDir, transform.forward) > fovDotProduct)
            {
                hits = Physics.RaycastAll(transform.position, target.transform.position - transform.position, detectionDistance);

                if (hits[0].transform.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        //Gizmos.DrawRay(transform.position, new Vector3(facingDirection.x + detectionRadius, 0, 0) * detectionDistance);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, detectionRadius, 0) * transform.forward * detectionDistance);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -detectionRadius, 0) * transform.forward * detectionDistance);
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);

        //Gizmos.DrawLine(transform.position, target.transform.position);

        Gizmos.DrawRay(transform.position, target.transform.position - transform.position);

    }
}
