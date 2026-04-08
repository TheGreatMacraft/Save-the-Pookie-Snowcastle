using UnityEngine;

public class NullTargetLocationSource : TargetLocationSource
{
    private readonly Location nullLocation = new NullLocation();

    public Vector3 Coordinates() => nullLocation.Coordinates();
}