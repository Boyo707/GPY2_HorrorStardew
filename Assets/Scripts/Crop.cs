using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class Crop : MonoBehaviour
{
    public string cropName = "Crop";
    public Sprite[] growthStages;

    [HideInInspector] public int growthCount = 0;
    [HideInInspector] public bool isMature = false;
    [HideInInspector] public bool isWateredToday = false;
    [HideInInspector] public Item plantedItem;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (growthStages == null || growthStages.Length == 0)
        {
            return;
        }
        UpdateStage();
    }

    public void Grow()
    {
        if (isMature) return;
        if (!isWateredToday) return;

        growthCount++;
        if (growthCount >= growthStages.Length - 1)
            isMature = true;

        UpdateStage();
    }

    public void Water()
    {
        isWateredToday = true;
        Debug.Log($"{cropName} has been watered");
    }

    public void ResetWatering()
    {
        isWateredToday = false;
    }

    private void UpdateStage()
    {
        int stageIndex = Mathf.Clamp(growthCount, 0, growthStages.Length - 1);
        if (spriteRenderer != null && growthStages[stageIndex] != null)
            spriteRenderer.sprite = growthStages[stageIndex];
    }
}
