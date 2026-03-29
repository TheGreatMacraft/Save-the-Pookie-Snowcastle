using UnityEngine;

public class NullLocation : Location
{
    public Vector3 Coordinates()
    {
        return Vector3.zero;
    }
}