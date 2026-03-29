using UnityEngine;

public class NullRotationDefinition : RotationDefinition
{
    private Quaternion quaternion = new Quaternion();
    
    public Quaternion Quaternion()
        => quaternion;
    
}