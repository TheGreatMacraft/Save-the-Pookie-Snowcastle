public sealed class ComplexSpeed
    : Speed
{
    private readonly float baseSpeed;
    private readonly AttributeModifier sprintModifier;
    

    public ComplexSpeed(
        float defaultSpeedMultiplier,
        AttributeModifier sprintModifier
        )
    {
        this.baseSpeed = new DefaultSpeed().Value() * defaultSpeedMultiplier;
        this.sprintModifier = sprintModifier;
    }


    public float Value()
        => baseSpeed * sprintModifier.Value();
}