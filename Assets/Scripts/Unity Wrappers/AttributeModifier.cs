public sealed class AttributeModifier
    : Scalar<float>, Condition
{
    private readonly float multiplier;
    private readonly Condition condition;


    public AttributeModifier(
        float multiplier,
        Condition condition
    )
    {
        this.multiplier =  multiplier;
        this.condition = condition;
    }
    
    
    public float Value()
        => condition.IsMet()
            ? multiplier
            : 1f;
    
    public bool IsMet()
        => condition.IsMet();
}