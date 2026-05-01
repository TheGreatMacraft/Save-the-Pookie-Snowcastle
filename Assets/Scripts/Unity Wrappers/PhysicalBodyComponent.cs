using UnityEngine;
[RequireComponent(typeof(Transform))]
[DisallowMultipleComponent]

public sealed class PhysicalBodyComponent : 
    MonoBehaviour,
    PhysicalBody
{
    // Location & Movement
    public Vector3 Coordinates()
        => transform.position;
    
    public void MoveTo(Location newLocation) 
    {
        transform.position = newLocation.Coordinates();
    }
    
    
    // Rotation
    public Quaternion Quaternion()
        => transform.rotation;
    
    public void RotateAs(Rotation newRotation)
    {
        transform.rotation = newRotation.Quaternion();
    }
    
    
    // Facing Direction Vector
    public Vector3 RawVector()
        => transform.right;

    public Location StartLocation()
        => this;
}