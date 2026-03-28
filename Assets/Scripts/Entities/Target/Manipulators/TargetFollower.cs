public class TargetFollower : Movement
{
    private Movement legs;

    public TargetFollower(
        Force movementForce,
        Location originLocation,
        TargetScouter targetScouter,
        float speed
    ) 
        : this(
            new Legs(
            movementForce,
            new Vector(
                new PointToPointVectorDefinition(
                    originLocation,
                    new CurrentTargetLocation(targetScouter)
                    )
                ),
            speed
            )) {}

    private TargetFollower(Legs legs)
    {
        this.legs = legs;
    }
    
    public void Move()
    {
        legs.Move();
    }
}