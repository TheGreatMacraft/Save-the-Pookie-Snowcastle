public class Legs : Movement
{
    private Force force;
    private Vector movementDirection;
    private float speed;


    public Legs(
        Force force,
        Vector movementDirection,
        float speed
        )
    {
        this.force = force;
        this.movementDirection = movementDirection;
        this.speed = speed;
    }


    public void Move()
    {
        force.AddConstant(movementDirection, speed);
    }
}