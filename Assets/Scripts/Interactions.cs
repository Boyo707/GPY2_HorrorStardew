using UnityEngine;

public class Interactions : MonoBehaviour
{
    public Camera playerCamera;
    public float interactRange = 5f;
    public float sphereRadius = 0.3f;

    [Header("Inventory")]
    public Inventory playerInventory;

    private CursorManager cursorManager;

    private void Start()
    {
        cursorManager = Object.FindFirstObjectByType<CursorManager>();
        if (cursorManager == null)
            Debug.LogWarning("cursor manager not found!");
    }

    private void Update()
    {
        HandleHover();
        if (Input.GetMouseButtonDown(0))
            TryInteract();
    }

    private void HandleHover()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereRadius, interactRange);

        if (hits.Length == 0)
        {
            cursorManager?.SetDefault();
            return;
        }

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            Plot plot = hit.collider.GetComponent<Plot>() ?? hit.collider.GetComponentInParent<Plot>();
            if (plot != null)
            {
                NPCTestInteraction draggedNPC = NPCTestInteraction.CurrentlyDraggedNPC;
                bool isDraggingNPC = draggedNPC != null && draggedNPC.CurrentState == NPCTestInteraction.TempNPCState.Dragging;

                if (isDraggingNPC && plot.currentState == Plot.PlotState.Planted)
                    cursorManager?.SetDefault();
                else
                {
                    switch (plot.currentState)
                    {
                        case Plot.PlotState.Empty: cursorManager?.SetTill(); break;
                        case Plot.PlotState.Tilled:
                            if (playerInventory != null && playerInventory.CurrentSeed != null)
                                cursorManager?.SetPlant();
                            else
                                cursorManager?.SetDefault();
                            break;
                        case Plot.PlotState.Planted:
                            if (plot.GetComponentInChildren<Crop>() != null && !plot.GetComponentInChildren<Crop>().isWateredToday)
                                cursorManager?.SetWater();
                            else
                                cursorManager?.SetDefault();
                            break;
                        case Plot.PlotState.Harvestable: cursorManager?.SetHarvest(); break;
                    }
                }
                return;
            }
        }

        cursorManager?.SetDefault();
    }

    private void TryInteract()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereRadius, interactRange);

        if (hits.Length == 0) return;

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            Plot plot = hit.collider.GetComponent<Plot>() ?? hit.collider.GetComponentInParent<Plot>();
            if (plot != null)
            {
                HandlePlotInteraction(plot);
                return;
            }
        }
    }

    private void HandlePlotInteraction(Plot plot)
    {
        NPCTestInteraction draggedNPC = NPCTestInteraction.CurrentlyDraggedNPC;
        bool isDraggingNPC = draggedNPC != null && draggedNPC.CurrentState == NPCTestInteraction.TempNPCState.Dragging;

        if (isDraggingNPC && plot.currentState == Plot.PlotState.Planted)
        {
            Crop crop = plot.GetComponentInChildren<Crop>();
            if (crop != null)
            {
                crop.growthCount = crop.growthStages.Length - 1;
                crop.isMature = true;
                crop.RefreshVisual();
                plot.currentState = Plot.PlotState.Harvestable;
                plot.UpdateVisual();

                plot.harvestedWithNPC = true;

                Destroy(draggedNPC.gameObject);
                NPCTestInteraction.CurrentlyDraggedNPC = null;
                return;
            }
        }

        if (!isDraggingNPC)
        {
            switch (plot.currentState)
            {
                case Plot.PlotState.Empty: plot.Till(); break;
                case Plot.PlotState.Tilled:
                    if (playerInventory != null && playerInventory.CurrentSeed != null)
                        plot.Plant(playerInventory.CurrentSeed);
                    else
                        Debug.LogWarning("No seed selected to plant");
                    break;
                case Plot.PlotState.Planted: plot.Water(); break;
                case Plot.PlotState.Harvestable: plot.Harvest(); break;
            }
        }
    }
}
