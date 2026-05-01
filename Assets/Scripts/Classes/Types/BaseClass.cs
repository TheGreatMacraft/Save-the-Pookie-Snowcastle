public abstract class BaseClass
    : Class
{
    private readonly ReadOnlyCollection<ActionExecution> abilities;
    private readonly float defaultSpeedMultiplier;
    private readonly float sprintSpeedMultiplier;


    public BaseClass(
        ReadOnlyCollection<ActionExecution> abilities,
        float defaultSpeedMultiplier,
        float sprintSpeedMultiplier
    )
    {
        this.abilities = abilities;
        this.defaultSpeedMultiplier = defaultSpeedMultiplier;
        this.sprintSpeedMultiplier = sprintSpeedMultiplier;
    }

    
    public ReadOnlyCollection<ActionExecution> Abilities()
        => abilities;
    public float DefaultSpeedMultiplier()
        => defaultSpeedMultiplier;
    public float SprintSpeedMultiplier()
        => sprintSpeedMultiplier;
}