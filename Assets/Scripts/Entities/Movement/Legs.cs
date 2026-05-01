public sealed class Legs 
    : Movement
{
    private readonly Force force;
    private readonly Vector movementDirection;
    private readonly Speed speed;


    public Legs(
        Force force,
        Vector movementDirection,
        Speed speed
        )
    {
        this.force = force;
        this.movementDirection = movementDirection;
        this.speed = speed;
    }
    

    public void Move()
    {
        force.AddConstant(movementDirection, speed.Value());
    }
}