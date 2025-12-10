using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponRelatedUtils
{
    public static void HitEnemy(GameObject enemyObject, WeaponBaseClass weaponThatAttacked, int damageAmount, Vector3 hitOrigin3D, float knockbackStrength)
    {
        // Decrease Enemy Health
        enemyObject.GetComponent<EnemyHealth>().DecreaseHealth(damageAmount, weaponThatAttacked);

        // Calculate Vector from Hit Origin to Enemy Position
        Vector2 enemyPos = new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y);
        Vector2 hitOrigin2D = new Vector2(hitOrigin3D.x, hitOrigin3D.y);
        Vector2 knockbackDirection = (enemyPos - hitOrigin2D).normalized;

        // Apply Knockback Velocity
        // INSERT: Enemy Movement Calls For Knockback

        //Temp Solution:
        Rigidbody2D enemyRb = enemyObject.GetComponent<Rigidbody2D>();
        enemyRb.velocity = knockbackDirection * knockbackStrength;
    }
}
