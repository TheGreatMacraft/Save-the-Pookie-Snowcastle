using System;
using UnityEngine;

public class EntityBasicMovement : MonoBehaviour
{
    // Variables to be Assigned in Inspector
    public float moveSpeed;
    
    [NonSerialized] public EntityAIBase AIBaseScript;
    [NonSerialized] public Rigidbody2D entityRb;
    
    // Used in Script
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
        if (AIBaseScript.currentState == EntityState.Pursuing && !isKnockedback)
            MoveTowardsTarget();
    }

    private void SetupComponents()
    {
        // AI Base Script
        if (AIBaseScript == null)
            AIBaseScript = GetComponent<EntityAIBase>();
        
        // Rigidbody
        if (entityRb == null)
            entityRb = GetComponent<Rigidbody2D>();
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