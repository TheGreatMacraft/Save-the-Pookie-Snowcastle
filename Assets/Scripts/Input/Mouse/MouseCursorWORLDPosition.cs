using System;
using UnityEngine;

public class MouseCursorWORLDPosition : Location
{
    private readonly Location location;
    
    
    public MouseCursorWORLDPosition(Camera camera)
        : this(new ScreenPositionAsWorldPosition(
            camera,
            new MouseCursorSCREENPosition()
            )) {}
    
    private MouseCursorWORLDPosition(Location location)
    {
        this.location = location;
    }
    
    
    public Vector3 Coordinates()
        => location.Coordinates();
}