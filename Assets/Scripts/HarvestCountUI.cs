using UnityEngine;
using TMPro;

public class HarvestCountUI : MonoBehaviour
{
    public Inventory playerInventory;

    [Header("UI References")]
    public TMP_Text pumpkinCountText;
    public TMP_Text tomatoCountText;

    [Header("Item References")]
    public Item pumpkinItem;
    public Item tomatoItem;

    void Update()
    {
        UpdateCounts();
    }

    void UpdateCounts()
    {
        if (playerInventory == null) return;

        int pumpkinCount = 0;
        int tomatoCount = 0;

        foreach (Item item in playerInventory.harvestedItems)
        {
            if (item == pumpkinItem) pumpkinCount++;
            else if (item == tomatoItem) tomatoCount++;
        }

        if (pumpkinCountText != null) pumpkinCountText.text = pumpkinCount.ToString();
        if (tomatoCountText != null) tomatoCountText.text = tomatoCount.ToString();
    }

}
