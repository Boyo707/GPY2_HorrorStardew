using UnityEngine;

[DisallowMultipleComponent]
public class Crop : MonoBehaviour
{
    public string cropName = "Crop";
    public int daysToGrow = 3;
    public GameObject[] growthStages;
    public bool isWateredToday = false;

    [HideInInspector] public int daysGrown = 0;
    [HideInInspector] public bool isMature = false;

    [HideInInspector] public Item plantedItem;

    private int currentStage = 0;

    private void Awake()
    {
        if (growthStages == null || growthStages.Length == 0)
        {
            Debug.LogError($"{cropName}: No growth stages assigned!");
            return;
        }

        foreach (GameObject stage in growthStages)
        {
            if (stage != null)
            {
                stage.SetActive(false);
                stage.transform.localScale = Vector3.one;
                stage.transform.localPosition = Vector3.zero;
                stage.transform.localRotation = Quaternion.identity;
            }
        }

        SetStage(0);
    }

    public void Grow()
    {
        if (isMature) return;

        daysGrown++;

        int stageCount = growthStages.Length;
        currentStage = Mathf.FloorToInt((float)daysGrown / daysToGrow * stageCount);
        currentStage = Mathf.Clamp(currentStage, 0, stageCount - 1);

        if (daysGrown >= daysToGrow)
            isMature = true;

        SetStage(currentStage);

        Debug.Log($"{cropName} grew to stage {currentStage} (Day {daysGrown}/{daysToGrow})");
    }

    public void Water()
    {
        isWateredToday = true;
        Debug.Log($"{cropName} has been watered!");
    }

    private void SetStage(int stageIndex)
    {
        if (growthStages == null || growthStages.Length == 0) return;

        for (int i = 0; i < growthStages.Length; i++)
        {
            if (growthStages[i] != null)
            {
                growthStages[i].SetActive(i == stageIndex);

                if (i == stageIndex)
                {
                    growthStages[i].transform.localPosition = Vector3.zero;
                    growthStages[i].transform.localRotation = Quaternion.identity;
                    growthStages[i].transform.localScale = Vector3.one;
                }
            }
        }
    }

    public void ResetWatering()
    {
        isWateredToday = false;
    }
}
