using UnityEngine;

public class InputAxisVectorDefinition : VectorDefinition
{
    private Location startLocation = new NullLocation();

    public Vector3 RawVector()
        => new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    
    public Location StartLocation()
        => startLocation;
}