using System;
using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[DisallowMultipleComponent]

public sealed class BasicEnemy : 
    MonoBehaviour,
    TargetLocation
{
    [SerializeField] private float speed;
    [SerializeField] private String targetTag;
    [Tooltip("The minimum proximity required to engage a target.")]
    [SerializeField] private float range;

    private TargetScouter targetScouter = new TargetScouter(
        new NullTargetPicker()
    );
    
    private Movement entityMovement;
    
    
    private void Awake()
    {
        Force entityMovementForce = new ComponentInObject<Force>(
            gameObject,
            new NullForce()
            ).Value();
        
        Location entityLocation = new ComponentInObject<Location>(
            gameObject,
            new NullLocation()
        ).Value();

        targetScouter = new TargetScouter(
            new ClosestTarget(
                entityLocation,
                targetTag
            )
        );

        entityMovement = new TargetFollower(
            entityMovementForce,
            entityLocation,
            targetScouter,
            new SimpleSpeed(speed),
            range
            );
        
        targetScouter.FindNewTarget();
    }

    private void FixedUpdate()
    {
        entityMovement.Move();
    }
    
    // Proxy
    public Vector3 Coordinates()
        => targetScouter.CurrentTarget().Coordinates();

    public Condition IsTargetFound()
        => targetScouter.IsTargetFound();
}