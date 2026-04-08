using System;
using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[DisallowMultipleComponent]

public sealed class BasicEnemy : 
    MonoBehaviour,
    TargetLocationSource
{
    [SerializeField] private float speed;
    [SerializeField] private String targetTag;
    [Tooltip("The minimum proximity required to engage a target.")]
    [SerializeField] private float range;

    private TargetScouter entityInstinct = new NullTargetScouter();
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

        entityInstinct = new EntityInstinct(
            entityLocation,
            targetTag
        );

        entityMovement = new TargetFollower(
            entityMovementForce,
            entityLocation,
            entityInstinct,
            speed,
            range
            );
        
        entityInstinct.FindNewTarget();
    }

    private void FixedUpdate()
    {
        entityMovement.Move();
    }
    
    // Proxy
    public Vector3 Coordinates()
        => entityInstinct.CurrentTarget().Coordinates();
}