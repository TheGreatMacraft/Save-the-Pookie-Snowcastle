using UnityEngine;

public sealed class MouseCursorFollower
    : Placement
{
    private readonly Placement mouseFollower;


    public MouseCursorFollower(
        Movable movable,
        Camera camera,
        ZCoordinate zCoordinate
    )
        : this(
            new SimplePlacement(
                movable,
                new MouseCursorWORLDPosition(camera)
            )
        ) {}
    
    private MouseCursorFollower(Placement mouseFollower)
    {
        this.mouseFollower = mouseFollower;
    }


    public void Place()
    {
        mouseFollower.Place();
    }
}