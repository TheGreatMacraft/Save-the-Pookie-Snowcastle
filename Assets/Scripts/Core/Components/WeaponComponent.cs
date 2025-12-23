using JetBrains.Annotations;
using UnityEngine;

public static class WeaponComponent
{
    public static void HitTarget(
        GameObject target,
        Vector3 hitOrigin3D,
        AttackActions attackingAttack,
        HitEssentials hitEssentials
        )
    {
        // Check that Target still Exists
        if(target == null) {return;}
        
        // Decrease Target Health
        target.GetComponent<HealthBase>().DecreaseHealth(hitEssentials.damageAmount, attackingAttack);
        
        if (hitEssentials.targetTag != "Enemy") {return;}
        
        // If Target is Enemy:
        
        // Calculate Vector from Hit Origin to Enemy Position
        var targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
        var hitOrigin2D = new Vector2(hitOrigin3D.x, hitOrigin3D.y);
        var knockbackDirection = (targetPos - hitOrigin2D).normalized;
            
        // Apply Knockback Velocity
        target.GetComponent<EnemyMovementBase>().
            ApplyKnockback(hitEssentials.knockbackStrength, knockbackDirection);
    }
}

