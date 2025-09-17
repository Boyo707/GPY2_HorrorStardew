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

    private Crop plantedCrop;
    private Renderer plotRenderer;

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
        Debug.Log("Plot tilled.");
    }

public void Plant(Item seedItem)
{
    if (currentState != PlotState.Tilled || seedItem == null || seedItem.cropPrefab == null) return;

    Vector3 spawnPos = transform.position + Vector3.up * 0.1f;
    plantedCrop = Instantiate(seedItem.cropPrefab, spawnPos, Quaternion.identity, transform);
    plantedCrop.transform.localPosition = Vector3.zero;
    plantedCrop.transform.localRotation = Quaternion.identity;
    plantedCrop.transform.localScale = Vector3.one;
    plantedCrop.gameObject.SetActive(true);

    plantedCrop.plantedItem = seedItem;

    currentState = PlotState.Planted;
    UpdateVisual();

    Inventory playerInventory = FindObjectOfType<Inventory>();
    if (playerInventory != null)
    {
        playerInventory.RemoveSeed(seedItem);
    }

    Debug.Log("planted " + seedItem.itemName);
}

    public void Water()
    {
        if (currentState != PlotState.Planted || plantedCrop == null) return;

        plantedCrop.Water();
    }

    public void Harvest()
    {
        if (currentState != PlotState.Harvestable || plantedCrop == null) return;

        Inventory playerInventory = FindObjectOfType<Inventory>();
        if (playerInventory != null && plantedCrop.plantedItem != null)
        {
            playerInventory.AddItem(plantedCrop.plantedItem); // reuse original ScriptableObject
        }

        Debug.Log("Harvested " + plantedCrop.cropName);

        Destroy(plantedCrop.gameObject);
        plantedCrop = null;
        currentState = PlotState.Tilled;
        UpdateVisual();
    }

    public void ProgressDay()
    {
        if (currentState == PlotState.Planted && plantedCrop != null)
        {
            plantedCrop.Grow();
            if (plantedCrop.isMature)
                currentState = PlotState.Harvestable;

            UpdateVisual();
        }
    }

    private void UpdateVisual()
    {
        if (plotRenderer == null) return;

        switch (currentState)
        {
            case PlotState.Empty:
                plotRenderer.material = emptyMat;
                break;
            case PlotState.Tilled:
                plotRenderer.material = tilledMat;
                break;
            case PlotState.Planted:
                plotRenderer.material = plantedMat;
                break;
            case PlotState.Harvestable:
                plotRenderer.material = harvestableMat;
                break;
        }
    }
}
