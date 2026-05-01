public interface Class
    : SpeedProvider
{
    public ReadOnlyCollection<ActionExecution> Abilities();
}

public interface SpeedProvider
{
    public float DefaultSpeedMultiplier();
    public float SprintSpeedMultiplier();
}