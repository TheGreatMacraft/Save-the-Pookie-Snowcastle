using UnityEngine;

public sealed class VectorRotationDefinition : RotationDefinition
{
    private readonly Vector vector;


    public VectorRotationDefinition(Vector vector)
    {
        this.vector = vector;
    }
    
    
    public Quaternion Quaternion()
        => UnityEngine.Quaternion.Euler(
            0f, 0f, vector.AngleInDegrees());
}