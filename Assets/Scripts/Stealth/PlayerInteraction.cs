using UnityEngine;

public interface IInteraction
{
    void OnInteraction(GameObject playerRefrence);
}

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject awareness;
    [SerializeField] private bool showGizmos = true;

    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] private bool isInteracting;

    [SerializeField] public bool isHunting;
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
                hitColliders[0].GetComponent<IInteraction>().OnInteraction(gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            isHunting = !isHunting;
            awareness.SetActive(isHunting);
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}
