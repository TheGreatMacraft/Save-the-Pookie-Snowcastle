using UnityEngine;

public sealed class ScreenPositionAsWorldPosition : Location
{
    private readonly Camera camera;
    private readonly Location screenLocation;
    

    public ScreenPositionAsWorldPosition(
        Camera camera,
        Location screenLocation
        )
    {
        this.camera = camera;
        this.screenLocation = screenLocation;
    }

    public Vector3 Coordinates()
        => camera.ScreenToWorldPoint(screenLocation.Coordinates());
}