using UnityEngine;

public class PointToPointVectorDefinition : VectorDefinition
{
    private readonly Location startLocation;
    private readonly Location endLocation;


    public PointToPointVectorDefinition(
        Location startLocation,
        Location endLocation
    )
    {
        this.startLocation = startLocation;
        this.endLocation = endLocation;
    }


    public Vector3 RawVector()
        => endLocation.Coordinates() - startLocation.Coordinates();

    public Location StartLocation()
        => startLocation;
}