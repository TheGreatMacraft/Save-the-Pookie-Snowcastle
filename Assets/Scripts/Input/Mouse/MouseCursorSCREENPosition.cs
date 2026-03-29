using System;
using UnityEngine;

public class MouseCursorSCREENPosition : Location
{
    public Vector3 Coordinates()
    {
        Vector3 unfilteredPos = Input.mousePosition;
        
        return new Vector3(
            Math.Clamp(unfilteredPos.x, 0, Screen.width),
            Math.Clamp(unfilteredPos.y, 0, Screen.height),
            -10f);
    }
}