using UnityEngine;

public sealed class Rotation
{
    private readonly RotationDefinition definition;


    public Rotation(RotationDefinition definition)
    {
        this.definition = definition;
    }
    
    
    public Quaternion Quaternion()
        => definition.Quaternion();
    
    public float Degrees()
        => Quaternion().eulerAngles.z;

    public Rotation AddDegrees(float degrees)
        => new Rotation(
            new ConstantRotationDefinition(Degrees() + degrees)
        );
    
    public bool Equals(Rotation other)
        => Quaternion().Equals(other.Quaternion());
}