using UnityEngine;

public class Interactions : MonoBehaviour
{
    public Camera playerCamera;
    public float interactRange = 5f;
    public float sphereRadius = 0.3f;

    [Header("Inventory")]
    public Inventory playerInventory;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * interactRange, Color.red, 2f);

        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereRadius, interactRange);

        if (hits.Length == 0)
        {
            Debug.Log("hit nothing");
            return;
        }

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        bool plotFound = false;

        foreach (RaycastHit hit in hits)
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);

            Plot plot = hit.collider.GetComponent<Plot>() ?? hit.collider.GetComponentInParent<Plot>();

            if (plot != null)
            {
                Debug.Log("hit plot: " + hit.collider.name + ", state = " + plot.currentState);
                HandlePlotInteraction(plot);
                plotFound = true;
                break;
            }
        }

        if (!plotFound)
        {
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
                    Debug.LogWarning("select seed bumass");
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