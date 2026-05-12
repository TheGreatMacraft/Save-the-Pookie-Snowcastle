public sealed class ClosestTarget
    : TargetPicker
{
    private readonly Location origin;
    private readonly string targetTag;
    
    private readonly Target nullTarget = new NullTarget();
    private Condition targetFound;


    public ClosestTarget(
        Location origin, 
        string targetTag
    )
    {
        this.origin = origin;
        this.targetTag = targetTag;
    }


    public Target Value()
    {
        ReadOnlyCollection<Target> targets 
            = new ComponentsInObjects<Target>(
            new GameObjectsWithTag(targetTag),
            nullTarget
        );

        return new ClosestLocation<Target>(
            origin,
            targets,
            nullTarget
        ).Value();
    }
}