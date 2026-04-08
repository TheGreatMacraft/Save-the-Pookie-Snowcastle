public sealed class NullTargetScouter : TargetScouter
{
    private readonly Target nullTarget = new NullTarget();
    
    public void FindNewTarget() {}
    public Target CurrentTarget() => nullTarget;
}