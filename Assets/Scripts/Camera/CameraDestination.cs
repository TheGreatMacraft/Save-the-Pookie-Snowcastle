using UnityEngine;

public class CameraDestination : Location
{
    private readonly VectorDefinition playerToMouse;
    private readonly float fraction;
    
    private readonly PointOnVector cameraDestination;


    public CameraDestination(
        VectorDefinition playerToMouse,
        float fraction)
    : this(new PointOnVector(playerToMouse, fraction)) {}

    private CameraDestination(PointOnVector pointOnVector)
    {
        this.cameraDestination = pointOnVector;
    }


    public Vector3 Coordinates()
        => cameraDestination.Coordinates();
}