using UnityEngine;

public interface IInteraction
{
    void OnInteraction();
}

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] private bool isInteracting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius, interactionLayer);

        if(hitColliders.Length != 0)
        {
            Debug.Log("can interact");

            if (Input.GetKeyDown(KeyCode.E))
            {
                //perform animation
                Debug.Log(hitColliders[0].name);
                hitColliders[0].GetComponent<IInteraction>().OnInteraction();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
