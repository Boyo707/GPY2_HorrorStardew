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

    Rigidbody playerRb;

    // Update is called once per frame
    void Update()
    {
        //It will play a small animation of the player killing the npc

        switch (currentState)
        {
            case TempNPCState.Idle:
                break;
            case TempNPCState.Corpse:
                break;
            case TempNPCState.Dragging:

                if(playerRb.linearVelocity != Vector3.zero)
                {
                    transform.position = Vector3.Lerp(transform.position, player.transform.position - playerRb.linearVelocity.normalized / 3, Time.deltaTime * 4);
                }

                sprite.flipX = transform.position.x < player.transform.position.x;

                break;
        }
    }

    public void OnInteraction(GameObject playerRefrence)
    {
        switch (currentState)
        {
            case TempNPCState.Idle:
                currentState += 1;
                player = playerRefrence;
                playerRb = player.GetComponent<Rigidbody>();
                awareness.GetComponent<NPCAwareness>().ToggleVisuals(false);
                awareness.SetActive(false);
                wonder.enabled = false;
                GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                //play kill animation

                break;
            case TempNPCState.Corpse:
                //transform.parent = player.transform;
                currentState += 1;
                break;
            case TempNPCState.Dragging:
                //transform.parent = null;
                currentState -= 1;
                break;
        }
    }
    
}
