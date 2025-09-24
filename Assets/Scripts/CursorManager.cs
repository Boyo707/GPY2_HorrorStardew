using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D tillCursor;
    public Texture2D plantCursor;
    public Texture2D waterCursor;
    public Texture2D harvestCursor;

    private void Start()
    {
        SetDefault();
    }

    public void SetDefault()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetTill()
    {
        Cursor.SetCursor(tillCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetPlant()
    {
        Cursor.SetCursor(plantCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetWater()
    {
        Cursor.SetCursor(waterCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetHarvest()
    {
        Cursor.SetCursor(harvestCursor, Vector2.zero, CursorMode.Auto);
    }
}
