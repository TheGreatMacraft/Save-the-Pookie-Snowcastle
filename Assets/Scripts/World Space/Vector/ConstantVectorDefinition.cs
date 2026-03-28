using UnityEngine;

public sealed class ConstantVectorDefinition :
    VectorDefinition
{
    private readonly Location startPosition;
    private readonly Vector3 vector;


    public ConstantVectorDefinition(Location startPosition, Location endPosition)
        : this(startPosition, 
            endPosition.Coordinates() - startPosition.Coordinates()) {}
    
    public ConstantVectorDefinition(Vector3 vector)
        : this(new NullLocation(), vector) {}

    public ConstantVectorDefinition(Location startPosition, Vector3 vector)
    {
        this.startPosition = startPosition;
        this.vector = vector;
    }
    
    
    public Vector3 RawVector()
        => vector;
    
    public Location StartLocation()
        => startPosition;
}