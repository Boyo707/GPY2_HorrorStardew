using UnityEngine;

public class NPCTestInteraction : MonoBehaviour, IInteraction
{
    public enum TempNPCState
    {
        Idle,       
        Corpse,     
        Dragging  
    }

    [Header("References")]
    [SerializeField] private GameObject awareness;
    [SerializeField] private NPCWonder wonder;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite aliveSprite;
    [SerializeField] private Sprite deadSprite;

    private GameObject player;
    private Rigidbody playerRb;

    private TempNPCState currentState = TempNPCState.Idle;

    public static NPCTestInteraction CurrentlyDraggedNPC;
    public TempNPCState CurrentState => currentState;

    private void Awake()
    {
        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (sprite != null && aliveSprite != null)
            sprite.sprite = aliveSprite;
        else
            Debug.LogWarning("SpriteRenderer or AliveSprite not assigned!");
    }

    private void Update()
    {
        switch (currentState)
        {
            case TempNPCState.Idle:
                break;

            case TempNPCState.Corpse:
                break;

            case TempNPCState.Dragging:
                if (playerRb != null && playerRb.linearVelocity != Vector3.zero)
                {
                    transform.position = Vector3.Lerp(
                        transform.position,
                        player.transform.position - playerRb.linearVelocity.normalized / 3,
                        Time.deltaTime * 4
                    );
                }

                if (sprite != null && player != null)
                    sprite.flipX = transform.position.x < player.transform.position.x;
                break;
        }
    }

    public void OnInteraction(GameObject playerReference)
    {
        switch (currentState)
        {
            case TempNPCState.Idle:
                currentState = TempNPCState.Corpse;
                player = playerReference;
                playerRb = player.GetComponent<Rigidbody>();

                if (awareness != null)
                {
                    NPCAwareness npcAwareness = awareness.GetComponent<NPCAwareness>();
                    if (npcAwareness == null)
                        npcAwareness = awareness.GetComponentInChildren<NPCAwareness>();

                    if (npcAwareness != null)
                        npcAwareness.ToggleVisuals(false);
                    else
                        Debug.LogWarning("No NPCAwareness component found on awareness object!");

                    awareness.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Awareness object not assigned in Inspector!");
                }

                if (wonder != null)
                    wonder.enabled = false;

                var rb = GetComponent<Rigidbody>();
                if (rb != null)
                    rb.linearVelocity = Vector3.zero;

                if (sprite != null && deadSprite != null)
                {
                    sprite.sprite = deadSprite;
                    Debug.Log("NPC killed → switched to DEAD sprite");
                }
                else
                {
                    Debug.LogWarning("SpriteRenderer or DeadSprite not assigned!");
                }
                break;

            case TempNPCState.Corpse:
                currentState = TempNPCState.Dragging;
                CurrentlyDraggedNPC = this;
                break;

            case TempNPCState.Dragging:
                currentState = TempNPCState.Corpse;
                CurrentlyDraggedNPC = null;
                break;
        }
    }

    private void OnDestroy()
    {
        if (CurrentlyDraggedNPC == this)
            CurrentlyDraggedNPC = null;
    }
}
