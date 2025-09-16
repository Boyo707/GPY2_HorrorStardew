using UnityEngine;

public class NPCAwareness : MonoBehaviour
{
    [SerializeField] private float noticeRadius;
    [SerializeField] private float maxSuspicionLevel;
    [SerializeField] private bool isScared;

    private Vector3 facingDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, noticeRadius);
        //could create a cone angle by using 3 lines. i just need a angle variable and a distance and then im set.
        //
    }
}
