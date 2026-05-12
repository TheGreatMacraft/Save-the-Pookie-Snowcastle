using UnityEngine;

public class NullTargetLocation: TargetLocation
{
    private readonly Location nullLocation = new NullLocation();

    public Vector3 Coordinates() => nullLocation.Coordinates();
    public Condition IsTargetFound() => new TrueCondition();
}