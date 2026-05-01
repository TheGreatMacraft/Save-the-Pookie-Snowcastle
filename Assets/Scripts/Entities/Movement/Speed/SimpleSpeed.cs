public sealed class SimpleSpeed
    : Speed
{
    private readonly float baseSpeed;
    

    public SimpleSpeed(
        float baseSpeed
        )
    {
        this.baseSpeed = baseSpeed;
    }


    public float Value()
        => baseSpeed;
}