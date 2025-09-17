using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory;

    [Header("UI References")]
    public Image seedIconImage;
    public TMP_Text seedCountText;

    void Update()
    {
        UpdateSelectedSeedUI();
    }

    void UpdateSelectedSeedUI()
    {
        if (playerInventory == null || playerInventory.CurrentSeed == null)
        {
            seedIconImage.enabled = false;
            if (seedCountText != null) seedCountText.text = "";
            return;
        }

        seedIconImage.enabled = true;
        seedIconImage.sprite = playerInventory.CurrentSeed.icon;

        if (seedCountText != null)
        {
            seedCountText.text = "x1"; // placeholder
        }
    }
}
