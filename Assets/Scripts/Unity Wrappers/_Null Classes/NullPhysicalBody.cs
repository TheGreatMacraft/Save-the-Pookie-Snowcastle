using UnityEngine;

public sealed class NullPhysicalBody : 
    PhysicalBody
{
    private readonly Location nullLocation = new NullLocation();
    private readonly RotationDefinition nullRotationDefinition = new NullRotationDefinition();
    private readonly VectorDefinition nullVectorDefinition = new NullVectorDefiniton();

    public Vector3 Coordinates() => nullLocation.Coordinates();
    public void MoveTo(Location newLocation) {}
    public Quaternion Quaternion() => nullRotationDefinition.Quaternion();
    public void RotateAs(Rotation newOrientation) {}
    public Vector3 RawVector() => nullVectorDefinition.RawVector();
    public Location StartLocation() => nullVectorDefinition.StartLocation();
}