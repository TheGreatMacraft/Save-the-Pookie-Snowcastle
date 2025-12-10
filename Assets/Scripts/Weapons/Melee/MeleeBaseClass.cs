using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public abstract class MeleeBaseClass : WeaponBaseClass
{
    // Values to be Set in Inspector
    public float hurtRadius;

    public float swingAngleDegrees;

    // Values to be changed in Script

    public float angle;

    public Vector2 hurtPoint;

    public override void Start()
    {
        base.Start();

        angle = swingAngleDegrees;
    }

    public override void AttackCall(InputAction.CallbackContext context)
    {
        if (CancelAttackCall(context)) { return;  }

        base.AttackCall(context);

        // Set the Swing Angle
        angle = SetSwingAngle();

        // Calculate Hurt Point
        hurtPoint = GetHurtPoint();

        Collider2D[] enemyColliders = GetEnemyCollidersInRadious();

        // Hit Every Enemy In Range
        HitInRadious(enemyColliders);

        // Swing Weapon
        StartCoroutine(RotateWeapon(angle, timeBetweenAttacks));
    }

    private float SetSwingAngle()
    {
        return angle * (-1);
    }

    private Vector2 GetHurtPoint()
    {
        // Calculate middle of Angle
        float halfAngle = Mathf.Deg2Rad * (angle / 2f);
        
        // Return Position of This Weapon + Vector of New Postion * hurtRadious
        Vector2 attackCenter = new Vector2(transform.position.x, transform.position.y);
        return attackCenter + new Vector2(Mathf.Cos(halfAngle), Mathf.Sin(halfAngle)) * hurtRadius;
    }

    public Collider2D[] GetEnemyCollidersInRadious()
    {
        // Set Layer Mask to target Only Enemies
        LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");

        //Filter through All Enemies in Radious of Point
        return Physics2D.OverlapCircleAll(hurtPoint, hurtRadius, enemyLayerMask);
    }

    public void HitInRadious(Collider2D[] enemyColliders)
    {
        foreach (Collider2D collider in enemyColliders)
        {
            WeaponRelatedUtils.HitEnemy(collider.gameObject, this, damage, transform.position, knockbackStrength);
        }
    }

    private IEnumerator RotateWeapon(float angle, float duration)
    {
        // Set Start and End rotation
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);

        yield return Utils.DoInTime(duration, (Action<float>)((progress) =>
        {
            // Rotate Weapon to Progress between Start and End Rotation
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, progress);
        }));

        // Ensure Weapon is rotated to End Rotation
        transform.rotation = endRotation;
    }
}
