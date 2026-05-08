using UnityEngine;

public sealed class NullLocation : Location
{
    public Vector3 Coordinates() => Vector3.zero;
    public bool IsSet() => false;
}