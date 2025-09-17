using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float dayLengthSeconds = 30f;
    private float dayTimer = 0f;
    public int currentDay = 1;

    private Plot[] allPlots;

    void Start()
    {
        allPlots = FindObjectsByType<Plot>(FindObjectsSortMode.None);
        Debug.Log("Day " + currentDay + " started.");
    }

    void Update()
    {
        dayTimer += Time.deltaTime;

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
