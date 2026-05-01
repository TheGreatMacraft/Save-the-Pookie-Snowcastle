public sealed class NullTargetPicker : TargetPicker
{
    private readonly Target nullTarget = new NullTarget();
    public Target Value() => nullTarget;
    
}