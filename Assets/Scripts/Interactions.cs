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
            Debug.LogWarning("cursormanager not found");
    }

    void Update()
    {
        HandleHover();
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
        }
    }

void HandleHover()
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
            switch (plot.currentState)
            {
                case Plot.PlotState.Empty:
                    cursorManager?.SetTill();
                    break;

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

                case Plot.PlotState.Harvestable:
                    cursorManager?.SetHarvest();
                    break;
            }
            return;
        }
    }

    cursorManager?.SetDefault();
}

    void TryInteract()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereRadius, interactRange);

        if (hits.Length == 0)
        {
            Debug.Log("hit nothing");
            return;
        }

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

    void HandlePlotInteraction(Plot plot)
    {
        switch (plot.currentState)
        {
            case Plot.PlotState.Empty:
                plot.Till();
                break;

            case Plot.PlotState.Tilled:
                if (playerInventory != null && playerInventory.CurrentSeed != null)
                    plot.Plant(playerInventory.CurrentSeed);
                else
                    Debug.LogWarning("No seed selected to plant");
                break;

            case Plot.PlotState.Planted:
                plot.Water();
                break;

            case Plot.PlotState.Harvestable:
                plot.Harvest();
                break;
        }
    }
}
