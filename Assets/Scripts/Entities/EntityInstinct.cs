using System.Collections.Generic;
using System.Linq;

public sealed class EntityInstinct : 
    TargetScouter
{
    private readonly Location origin;
    private readonly string targetTag;
    
    private Target currentTarget;
    
    
    public  EntityInstinct(
        Location origin,
        string targetTag
        )
    {
        this.origin = origin;
        this.targetTag = targetTag;
    }


    public void FindNewTarget()
    {
        IEnumerable<Target> targets = new ComponentsInObjects<Target>(
            new GameObjectsWithTag(targetTag).AllElements(),
            new NullTarget()
        ).AllElements();

        Index newTargetIndex = new ClosestLocationIndex(
            origin,
            targets
        );
        
        currentTarget = targets.ElementAt(newTargetIndex.Value());
    }
    
    public Target CurrentTarget()
        => currentTarget;
}