using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Day Settings")]
    public float dayLengthSeconds = 30f;
    private float dayTimer = 0f;
    public int currentDay = 1;

    [Header("Day-Night")]
    public DayNightCycle dayNightCycle;

    private Plot[] allPlots;

    void Start()
    {
        allPlots = Object.FindObjectsByType<Plot>(FindObjectsSortMode.None);
        if (dayNightCycle == null) dayNightCycle = Object.FindFirstObjectByType<DayNightCycle>();

        dayTimer = 0.25f * dayLengthSeconds;

        if (dayNightCycle != null)
        {
            float dayProgress = dayTimer / dayLengthSeconds;
            dayNightCycle.SetDayProgress(dayProgress);
        }

        Debug.Log("Day " + currentDay + " started (morning).");
    }


    void Update()
    {
        dayTimer += Time.deltaTime;

        if (dayNightCycle != null)
        {
            float dayProgress = dayTimer / dayLengthSeconds;
            dayNightCycle.SetDayProgress(dayProgress);
        }

        if (dayTimer >= dayLengthSeconds)
        {
            AdvanceDay();
            dayTimer = 0f;
        }
    }

    void AdvanceDay()
    {
        currentDay++;
        Debug.Log("Day " + currentDay + " started.");

        foreach (Plot plot in allPlots)
        {
            plot.ProgressDay();
        }
    }
}
