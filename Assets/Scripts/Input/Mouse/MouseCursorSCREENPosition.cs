using System;
using UnityEngine;

public sealed class MouseCursorSCREENPosition : Location
{
    private readonly ZCoordinate zCoordinate;

    
    

    public MouseCursorSCREENPosition(ZCoordinate zCoordinate)
    {
        this.zCoordinate = zCoordinate;
    }
    
    
    public Vector3 Coordinates()
    {
        Vector3 unfilteredPos = Input.mousePosition;
        
        return new Vector3(
            Math.Clamp(unfilteredPos.x, 0, Screen.width),
            Math.Clamp(unfilteredPos.y, 0, Screen.height),
            zCoordinate.Value()
        );
    }
}