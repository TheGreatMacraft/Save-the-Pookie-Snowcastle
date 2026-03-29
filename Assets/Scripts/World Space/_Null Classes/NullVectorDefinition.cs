using UnityEngine;

public class NullVectorDefiniton : VectorDefinition
{
    private readonly Location startLocation = new NullLocation();
    
    public Vector3 RawVector() 
        => Vector3.zero;
    public Location StartLocation()
        => startLocation;
}