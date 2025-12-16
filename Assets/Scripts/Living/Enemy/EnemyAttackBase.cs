using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    // External Objects Necessary
    public EnemyAIBase AIBaseScript;
    
    // Variables used in Script
    public WeaponBase weaponScript;
    protected void Start()
    {
        SetupComponents();
    }

    protected void SetupComponents()
    {
        // AI Base Script
        if (AIBaseScript == null)
            AIBaseScript = GetComponent<EnemyAIBase>();
        
        // Weapon Script
        if (weaponScript == null)
            weaponScript = GetComponent<WeaponBase>();
    }

    protected void Update()
    {
        if(AIBaseScript.currentTarget == null) {return;}

        // Attempt Attack, if Current State is Attacking
        if (AIBaseScript.currentState == EnemyState.Attacking)
        {
           AttemptAttacking();
        }
    }

    public virtual void AttemptAttacking()
    {
        // Attack After Cooldown, toggle executingAttack
        Utils.ToggleBoolInTime(
            v => executingAttack = v,
            executingAttack,
            enemyAttackScript.,
            (Action)TriggerAttack);
    }

    protected void TriggerAttack()
    {
        // Get Colliders in Attack Radious
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadious);
        
        // Attack Appropriate Colliders
        foreach (Collider2D hitCollider in hitColliders)
        {
            if(hitCollider.CompareTag(AIBaseScript.pursuingTargetTag))
                AttackObject(hitCollider.gameObject);
        }
        
        // Set State
        AIBaseScript.currentState = AIBaseScript.NearbyTarget() ? EnemyState.Attacking : EnemyState.Pursuing;
    }

    public void AttackObject(GameObject target)
    {
        target.GetComponent<HealthBase>().DecreaseHealth(damage);
        
        if (target.CompareTag("Player"))
        {
            //INSERT: Display Hurt Animation
        }
    }
}