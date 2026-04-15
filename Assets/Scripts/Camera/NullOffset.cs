using UnityEngine;

public sealed class NullOffset : Offset
{
    public Vector3 Coordinates() => Vector3.zero;
}