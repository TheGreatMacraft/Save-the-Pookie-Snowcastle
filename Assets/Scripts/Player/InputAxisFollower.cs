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

    public bool isMoving()
        => !inputAxisVector.Equals(nullVector);

    public void Move()
    {
        if (!isMoving()) return;

        legs.Move();
    }
}