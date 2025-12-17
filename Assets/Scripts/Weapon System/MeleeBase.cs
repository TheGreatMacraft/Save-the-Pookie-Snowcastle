using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class MeleeBase : WeaponBase
{
    // Variables to be Assigned in Inspector
    public string opponentTag;

    // Variables used in Script
    public Transform attackPoint;
    

    public void Attack()
    {
        // Get all Opponents In Range
        GameObject[] opponentsInRange = 
            Utils.GetObjectsInRadiousWithTag(attackPoint.position, weaponRange,opponentTag);

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
                damageAmount,
                transform.position,
                knockbackStrength,
                knockbackDuration,
                this,
                opponentTag
            );
    }

    protected override Dictionary<string, Action> WeaponActionFunctions()
    {
        return new Dictionary<string, Action>()
        {
            { "Attack", Attack }
        };
    }
}