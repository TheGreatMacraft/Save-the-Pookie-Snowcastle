using System;
using UnityEngine;

public abstract class EnemyMovementBase : MovementBase
{
    // Used in Script
    public EnemyAIBase AIBaseScript;
    
    public bool isKnockedback;

    
    private void Start()
    {
        SetupComponents();
    }

    public void FixedUpdate()
    {
        // If there is no Target, Stop
        if(AIBaseScript.currentTarget == null) {return;}
        
        // If Target not within range, Move towards it
        if (AIBaseScript.currentState == EnemyState.Pursuing && !isKnockedback)
            MoveTowardsTarget();
    }

    private void SetupComponents()
    {
        // Rigidbody
        if (entityRb == null)
            entityRb = GetComponent<Rigidbody2D>();
        
        // AI Base Script
        if (AIBaseScript == null)
            AIBaseScript = GetComponent<EnemyAIBase>();
    }

    public void MoveTowardsTarget()
    {
        Vector2 movementDirection = (AIBaseScript.currentTarget.transform.position - transform.position).normalized;
        entityRb.AddForce(movementDirection * moveSpeed, ForceMode2D.Impulse);
    }

    public void ApplyKnockback(float force, Vector2 knockbackDirection)
    {
        entityRb.AddForce(knockbackDirection * force, ForceMode2D.Impulse);
    }
}