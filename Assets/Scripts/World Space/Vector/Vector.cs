using UnityEngine;

public sealed class Vector
{
    private readonly VectorDefinition definition;
    

    public Vector(VectorDefinition definition)
    {
        this.definition = definition;
    }
    

    public Location StartLocation()
        => definition.StartLocation();
    
    public Vector3 RawVector() 
        => definition.RawVector();
    
    public Vector3 Direction()
        => RawVector().normalized;
    
    public float Magnitude()
        => RawVector().magnitude;
    
    public float AngleInDegrees()
        => Mathf.Atan2(
            RawVector().y,
            RawVector().x
        ) * Mathf.Rad2Deg;

    public Vector Reverse()
        => new Vector(
            new PointToPointVectorDefinition(
                new Position(
                    StartLocation().Coordinates()
                    + RawVector()
                ),
                StartLocation()
            )
        );
    
    public bool Equals(Vector other)
        => RawVector().Equals(other.RawVector());
}