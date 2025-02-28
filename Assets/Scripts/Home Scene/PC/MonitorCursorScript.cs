using UnityEngine;

public class MonitorCursorScript : MonoBehaviour
{
    public Texture2D cursorOverPC;  // Cursor when over the PC/monitor
    public Texture2D defaultCursor; // Default cursor

    private void OnMouseEnter()
    {
        // Change cursor when the mouse enters the monitor area
        Cursor.SetCursor(cursorOverPC, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        // Revert to the default cursor when the mouse leaves the monitor area
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
