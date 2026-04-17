public sealed class InputAxisFollower 
    : ComplexMovement
{
    private readonly Movement legs;
    private readonly Vector inputAxisVector;
    private readonly Vector nullVector;

    
    public InputAxisFollower(Force movement, float speed)
    {
        this.inputAxisVector = new Vector(new InputAxisVectorDefinition());
        this.nullVector = new Vector(new NullVectorDefiniton());
        
        this.legs = new Legs(
            movement,
            this.inputAxisVector,
            speed
        );
    }

    public bool IsMet()
        => !inputAxisVector.Equals(nullVector);

    public void Move()
    {
        if (!IsMet()) return;

        legs.Move();
    }
}