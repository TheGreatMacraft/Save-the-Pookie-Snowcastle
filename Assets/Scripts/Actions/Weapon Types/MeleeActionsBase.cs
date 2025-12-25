using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeActionsBase : AttackActions
{
    // Variables used in Script
    public Transform attackPoint;

    protected override void Awake()
    {
        base.Awake();
        
        SetupComponents();
    }

    protected virtual void SetupComponents()
    {
        // Attack Point
        if(attackPoint  == null)
            attackPoint = transform;
    }

    protected override void Attack()
    {
        // Get all Opponents In Range
        GameObject[] opponentsInRange = 
            Utils.GetObjectsInRadiousWithTag(attackPoint.position, actionRange, hitEssentials.targetTag);

        // Hit Every Enemy In Range
        HitInRadious(opponentsInRange);

        // Swing Weapon
        // Play Swing Animation
    }

    public void HitInRadious(GameObject[] opponentsInRange)
    {
        foreach (var opponent in opponentsInRange)
            WeaponComponent.HitTarget(
                opponent,
                transform.position,
                this,
                hitEssentials
            );
    }
}