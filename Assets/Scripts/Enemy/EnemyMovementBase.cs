using System;
using UnityEngine;

public abstract class EnemyMovementBase : MonoBehaviour
{
    // Variables to be Assigned in Inspector
    public float moveSpeed;

    // External Objects Necessary
    public Rigidbody2D enemyRb;
    public EnemyAIBase AIBaseScript;

    // Variables used in Script
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
        if (enemyRb == null)
            enemyRb = GetComponent<Rigidbody2D>();
        
        // AI Base Script
        if (AIBaseScript == null)
            AIBaseScript = GetComponent<EnemyAIBase>();
    }

    public void MoveTowardsTarget()
    {
        Vector2 movementDirection = (AIBaseScript.currentTarget.transform.position - transform.position).normalized;
        enemyRb.AddForce(movementDirection * moveSpeed, ForceMode2D.Impulse);
    }

    public void ApplyKnockback(float force, Vector2 knockbackDirection)
    {
        enemyRb.AddForce(knockbackDirection * force, ForceMode2D.Impulse);
    }
}