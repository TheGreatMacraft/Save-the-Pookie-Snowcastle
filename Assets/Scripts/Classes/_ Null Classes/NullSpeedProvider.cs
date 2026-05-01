public sealed class NullSpeedProvider : SpeedProvider
{
    public float DefaultSpeedMultiplier() => 1f;
    public float SprintSpeedMultiplier() => 1f;
}