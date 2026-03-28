using UnityEngine;

public class CurrentTargetLocation : Location
{
    private readonly TargetScouter targetSource;
    
    
    public  CurrentTargetLocation(TargetScouter targetSource)
    {
        this.targetSource = targetSource;
    }
    
    
    public Vector3 Coordinates()
        => targetSource.CurrentTarget().Coordinates();
}