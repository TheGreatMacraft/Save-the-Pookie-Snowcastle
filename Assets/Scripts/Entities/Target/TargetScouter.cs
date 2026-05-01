public sealed class TargetScouter
{
    private readonly TargetPicker targetPicker;
    private Target currentTarget;
    
    
    public TargetScouter(TargetPicker targetLocator)
        : this(targetLocator, new  NullTarget()) {}
    
    private TargetScouter(
        TargetPicker targetPicker,
        Target currentTarget
        )
    {
        this.targetPicker = targetPicker;
        this.currentTarget = currentTarget;
    }


    public void FindNewTarget()
    {
        currentTarget = targetPicker.Value();
    }
    
    public Target CurrentTarget()
        => currentTarget;
}