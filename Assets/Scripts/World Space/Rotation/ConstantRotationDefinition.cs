using UnityEngine;

public class ConstantRotationDefinition : 
    RotationDefinition
{
    private readonly Quaternion rotation;
    
    
    public ConstantRotationDefinition(Vector vector)
        : this(vector.AngleInDegrees()) {}
    public ConstantRotationDefinition(float angleInDegrees)
        : this(UnityEngine.Quaternion.Euler(0f, 0f, angleInDegrees)) {}
    public ConstantRotationDefinition(Quaternion rotation)
    {
        this.rotation = rotation;
    }
    
    
    public Quaternion Quaternion()
        => rotation;
}