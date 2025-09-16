using UnityEngine;

public class NPCTestInteraction : MonoBehaviour, IInteraction
{
    public enum TempNPCState
    {
        Idle,
        Corpse,
        Dragging
    }

    [SerializeField] private float interactionRange;
    [SerializeField] private GameObject player;

    private TempNPCState currentState = TempNPCState.Idle;

    // Update is called once per frame
    void Update()
    {
        //It will play a small animation of the player killing the npc

        switch (currentState)
        {
            case TempNPCState.Idle:
                //play animation
                break;
            case TempNPCState.Corpse:
                transform.parent = null;
                break;
            case TempNPCState.Dragging:
                transform.parent = player.transform;
                break;
        }
    }

    public void OnInteraction()
    {
        switch (currentState)
        {
            case TempNPCState.Idle:
                currentState += 1;
                transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 90);
                break;
            case TempNPCState.Corpse:
                currentState += 1;
                break;
            case TempNPCState.Dragging:
                currentState -= 1;
                break;
        }
    }
    
}
