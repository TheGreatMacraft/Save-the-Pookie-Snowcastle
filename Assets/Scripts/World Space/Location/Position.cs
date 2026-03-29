using UnityEngine;

public sealed class Position : Location
{
    private readonly Vector3 coordinates;


    public Position(Vector3 coordinates)
    {
        this.coordinates = coordinates;
    }
    
    
    public Vector3 Coordinates() => coordinates;
}