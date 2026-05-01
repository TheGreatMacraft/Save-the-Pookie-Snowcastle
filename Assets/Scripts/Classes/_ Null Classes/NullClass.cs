public sealed class NullClass : Class
{
    private ReadOnlyCollection<ActionExecution> abilities 
        = new SimpleReadOnlyCollection<ActionExecution>();
    
    public ReadOnlyCollection<ActionExecution> Abilities() => abilities;
    
    public float DefaultSpeedMultiplier() => 1f;
    public float SprintSpeedMultiplier() => 1.25f;
}