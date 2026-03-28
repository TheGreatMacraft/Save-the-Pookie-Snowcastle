using UnityEngine;

public sealed class BodyFacingDirectionVectorDefinition : 
    VectorDefinition
{
    private readonly Transform body;
    private readonly Location BodyLocation;

    
    public BodyFacingDirectionVectorDefinition(
        Transform body,
        Location startPosition
        )
    {
        this.body = body;
        this.BodyLocation = startPosition;
    }


    public Vector3 RawVector()
        => body.right;
    
    public Location StartLocation()
        => BodyLocation;
}