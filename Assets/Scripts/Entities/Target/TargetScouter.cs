public sealed class TargetScouter
{
    private readonly TargetPicker targetPicker;
    
    private Target currentTarget;
    private Target nullTarget;
    
    private Condition isTargetFound;
    private Condition currentTargetExists;
    
    
    public TargetScouter(TargetPicker targetLocator)
        : this(targetLocator, new  NullTarget()) {}
    
    private TargetScouter(
        TargetPicker targetPicker,
        Target nullTarget
        )
    {
        this.targetPicker = targetPicker;
        this.nullTarget = nullTarget;
        this.currentTarget = nullTarget;
    }


    public void FindNewTarget() => currentTarget = targetPicker.Value();

    public Target CurrentTarget() => currentTarget;

    public Condition IsTargetFound()
        => isTargetFound ??=
            new IsTrue(() =>
            {
                if(currentTarget == nullTarget || !currentTarget.Exists())
                    FindNewTarget();
                    
                return currentTarget != nullTarget && currentTarget.Exists();
            });
}