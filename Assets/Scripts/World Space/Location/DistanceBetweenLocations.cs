public sealed class DistanceBetweenLocations
{
    private readonly Vector pointToPointVector;
    
    public  DistanceBetweenLocations(
        Location startLocation,
        Location endLocation
        )
    : this(new Vector(
        new PointToPointVectorDefinition(
            startLocation,
            endLocation
            ))) {}

    private DistanceBetweenLocations(Vector pointToPointVector)
    {
        this.pointToPointVector = pointToPointVector;
    }


    public float Value()
        => pointToPointVector.Magnitude();

}