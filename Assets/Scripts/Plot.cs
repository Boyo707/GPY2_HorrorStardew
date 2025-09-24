using UnityEngine;

public class Plot : MonoBehaviour
{
    public enum PlotState { Empty, Tilled, Planted, Harvestable }
    public PlotState currentState = PlotState.Empty;

    [Header("Materials")]
    public Material emptyMat;
    public Material tilledMat;
    public Material plantedMat;
    public Material harvestableMat;
    public Material wateredMat;

    [HideInInspector] public bool harvestedWithNPC = false;

    private Crop plantedCrop;
    private Renderer plotRenderer;
    private Item plantedSeedItem;

    void Start()
    {
        plotRenderer = GetComponent<Renderer>();
        UpdateVisual();
    }

    public void Till()
    {
        if (currentState != PlotState.Empty) return;
        currentState = PlotState.Tilled;
        UpdateVisual();
    }

    public void Plant(Item seedItem)
    {
        if (currentState != PlotState.Tilled || seedItem == null || seedItem.cropPrefab == null) return;

        plantedCrop = Instantiate(seedItem.cropPrefab, transform.position, Quaternion.identity, transform);
        plantedCrop.transform.localPosition = new Vector3(0, 6, 0);
        plantedCrop.transform.localScale = new Vector3(1.634f, 13.454f, 4.022f);
        plantedCrop.plantedItem = seedItem;
        plantedSeedItem = seedItem;

        currentState = PlotState.Planted;
        UpdateVisual();
    }

    public void Water()
    {
        if (currentState != PlotState.Planted || plantedCrop == null) return;
        plantedCrop.Water();
        UpdateVisual();
    }

    public void Harvest()
    {
        if (currentState != PlotState.Harvestable || plantedCrop == null) return;

        Inventory playerInventory = Object.FindFirstObjectByType<Inventory>();
        if (playerInventory != null && plantedSeedItem != null && plantedSeedItem.harvestProduct != null)
        {
            int amountToGive = harvestedWithNPC ? 3 : 1;
            for (int i = 0; i < amountToGive; i++)
            {
                playerInventory.AddItem(plantedSeedItem.harvestProduct);
            }
        }

        Destroy(plantedCrop.gameObject);
        plantedCrop = null;
        plantedSeedItem = null;
        currentState = PlotState.Tilled;
        UpdateVisual();

        harvestedWithNPC = false;
    }

    public void ProgressDay()
    {
        if (currentState == PlotState.Planted && plantedCrop != null)
        {
            plantedCrop.Grow();
            plantedCrop.ResetWatering();
            if (plantedCrop.isMature) currentState = PlotState.Harvestable;
            UpdateVisual();
        }
    }

    public void UpdateVisual()
    {
        if (plotRenderer == null) return;

        if (currentState == PlotState.Planted && plantedCrop != null && plantedCrop.isWateredToday)
        {
            plotRenderer.material = wateredMat;
            return;
        }

        switch (currentState)
        {
            case PlotState.Empty: plotRenderer.material = emptyMat; break;
            case PlotState.Tilled: plotRenderer.material = tilledMat; break;
            case PlotState.Planted: plotRenderer.material = plantedMat; break;
            case PlotState.Harvestable: plotRenderer.material = harvestableMat; break;
        }
    }
}
