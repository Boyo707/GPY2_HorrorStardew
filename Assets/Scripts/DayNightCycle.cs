using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    [Header("Sun Settings")]
    public float noonAngle = 180f;
    public Color dayColor = Color.white;
    public Color nightColor = new Color(0.2f, 0.2f, 0.35f);

    void Start()
    {
        if (sun == null) sun = Object.FindFirstObjectByType<Light>();
    }

    public void SetDayProgress(float progress)
    {
        float sunAngle = Mathf.Lerp(-90f, 270f, progress);
        sun.transform.rotation = Quaternion.Euler(sunAngle, 0, 0);

        if (progress >= 0.25f && progress <= 0.75f)
        {
            float t = Mathf.InverseLerp(0.25f, 0.75f, progress);
            sun.intensity = Mathf.Lerp(0.2f, 1f, t);
            sun.color = Color.Lerp(new Color(1f, 0.7f, 0.5f), dayColor, t);
        }
        else
        {
            float t = progress < 0.25f ? Mathf.InverseLerp(0f, 0.25f, progress) : Mathf.InverseLerp(0.75f, 1f, progress);
            sun.intensity = Mathf.Lerp(0f, 0.2f, t);
            sun.color = nightColor;
        }
    }
}
