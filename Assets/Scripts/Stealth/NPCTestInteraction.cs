using UnityEngine;

public class NPCTestInteraction : MonoBehaviour, IInteraction
{
    public enum TempNPCState
    {
        Idle,
        Corpse,
        Dragging
    }

    [SerializeField] private GameObject awareness;
    [SerializeField] private NPCWonder wonder;
    [SerializeField] private SpriteRenderer sprite;

    private GameObject player;
    private TempNPCState currentState = TempNPCState.Idle;
    private Rigidbody playerRb;

    public static NPCTestInteraction CurrentlyDraggedNPC;

    public TempNPCState CurrentState => currentState;

    void Update()
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
                awareness.GetComponent<NPCAwareness>().ToggleVisuals(false);
                awareness.SetActive(false);
                wonder.enabled = false;
                GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
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
