using UnityEngine;

public sealed class MouseCursorVisibility : Visibility
{
    private Condition isVisible;
    public void Hide() => Cursor.visible = false;
    public void Show() => Cursor.visible = true;
    public Condition IsVisible() => isVisible ??= new IsTrue(() => Cursor.visible);
}