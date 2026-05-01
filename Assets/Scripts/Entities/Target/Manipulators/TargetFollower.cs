public sealed class TargetFollower : 
    ComplexMovement
{
    private readonly Movement legs;
    private readonly DistanceBetweenLocations distanceToTarget;
    private readonly float range;

    public TargetFollower(
        Force movementForce,
        Location originLocation,
        TargetScouter targetScouter,
        Speed speed,
        float range
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
            ),
            new DistanceBetweenLocations(
                originLocation,
                new CurrentTargetLocation(targetScouter)
                ),
            range
            ) {}

    private TargetFollower(
        Legs legs,
        DistanceBetweenLocations distanceToTarget,
        float range
        )
    {
        this.legs = legs;
        this.distanceToTarget = distanceToTarget;
        this.range = range;
    }

    public bool IsMet()
        => distanceToTarget.Value() > range;

    public void Move()
    {
        if (!IsMet()) return;
        
        legs.Move();
    }
}