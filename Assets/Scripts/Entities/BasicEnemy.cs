using System;
using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[DisallowMultipleComponent]

public sealed class BasicEnemy : 
    MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private String targetTag;
    
    private TargetScouter entityInstinct;
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
            speed
            );
        
        entityInstinct.FindNewTarget();
    }

    private void FixedUpdate()
    {
        entityMovement.Move();
    }
}