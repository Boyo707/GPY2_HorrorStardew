using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("Seeds")]
    public Item[] seedItems;
    private int currentSeedIndex = 0;
    public int seedCount = 1;

    [Header("Harvested Items")]
    public List<Item> harvestedItems = new List<Item>();

    public Item CurrentSeed => seedItems.Length > 0 ? seedItems[currentSeedIndex] : null;

    void Update()
    {
        if (seedItems.Length == 0) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) NextSeed();
        else if (scroll < 0f) PreviousSeed();
    }

    public void NextSeed()
    {
        if (seedItems.Length == 0) return;
        currentSeedIndex = (currentSeedIndex + 1) % seedItems.Length;
        Debug.Log("Selected seed: " + CurrentSeed.itemName);
    }

    public void PreviousSeed()
    {
        if (seedItems.Length == 0) return;
        currentSeedIndex = (currentSeedIndex - 1 + seedItems.Length) % seedItems.Length;
        Debug.Log("Selected seed: " + CurrentSeed.itemName);
    }

    public void AddItem(Item item)
    {
        if (item == null) return;
        harvestedItems.Add(item);
        Debug.Log("Added to inventory: " + item.itemName);
    }

    public void RemoveSeed(Item seedItem)
    {
        if (seedItem == null) return;

        for (int i = 0; i < seedItems.Length; i++)
        {
            if (seedItems[i] == seedItem)
            {
                seedCount--;
                if (seedCount <= 0)
                {
                    var tempList = new List<Item>(seedItems);
                    tempList.RemoveAt(i);
                    seedItems = tempList.ToArray();
                    currentSeedIndex = 0;
                    seedCount = 1;
                }
                break;
            }
        }
    }
}
