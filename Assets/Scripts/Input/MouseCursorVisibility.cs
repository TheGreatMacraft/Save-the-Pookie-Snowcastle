using UnityEngine;

public class MouseCursorVisibility : Visibility
{
    public void Hide() => Cursor.visible = false;
    public void Show() => Cursor.visible = true;
}