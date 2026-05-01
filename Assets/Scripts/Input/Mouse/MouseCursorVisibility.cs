using UnityEngine;

public sealed class MouseCursorVisibility : Visibility
{
    public void Hide() => Cursor.visible = false;
    public void Show() => Cursor.visible = true;
    public bool IsMet() => Cursor.visible;
}