using UnityEngine;

public class PointOnVector : Location
{
    private readonly VectorDefinition definition;
    private readonly float fraction;


    public PointOnVector(
        VectorDefinition definition,
        float fraction
        )
    {
        this.definition = definition;
        this.fraction = fraction;
    }


    public Vector3 Coordinates()
        => Vector3.Lerp(
            definition.StartLocation().Coordinates(),
            definition.StartLocation().Coordinates() + definition.RawVector(),
            fraction
        );
}