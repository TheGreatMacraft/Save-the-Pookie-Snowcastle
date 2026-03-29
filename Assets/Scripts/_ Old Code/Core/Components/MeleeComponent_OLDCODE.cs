using UnityEngine;

public static class MeleeComponent_OLDCODE
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
        target.GetComponent<Damageable>().TakeDamage(hitEssentials.damageAmount);
        
        if (hitEssentials.affectedObjectsTag != "Enemy") {return;}
        
        // If Target is Enemy:
        
        // Calculate Vector from Hit Origin to Enemy Position
        var targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
        var hitOrigin2D = new Vector2(hitOrigin3D.x, hitOrigin3D.y);
        var knockbackDirection = (targetPos - hitOrigin2D).normalized;
            
        // Apply Knockback Velocity
        target.GetComponent<EntityBasicMovement>().
            ApplyKnockback(hitEssentials.knockbackStrength, knockbackDirection);
    }
}

