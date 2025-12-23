using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public static class ShootingComponent
{
    public static Quaternion GetAimRotation(Vector3 source, Vector3 target)
    {
        // Calculate Vector From Weapon To Mouse Cursor
        Vector2 targetFacingVector = (target - source).normalized;

        // Return Rotation From Source to Target
        var targetFacingAngle = Mathf.Atan2(targetFacingVector.y, targetFacingVector.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, targetFacingAngle);
    }
    
    public static void SpawnProjectile(
        GameObject projectilePrefab,
        Quaternion originRotation,
        Vector3 projectileSpawnPoint,
        float projectileSpeed,
        HitEssentials hitEssentials,
        GunActionsBase ownerGun = null,
        [CanBeNull] float? spread = null,
        [CanBeNull] List<GameObject> projectilesShot = null)
    {
        
        // Calculate Angle with Spread
        float spreadAngle = spread.HasValue ? Random.Range(-spread.Value, spread.Value) : 0;

        // Calculate Rotation for Applied Velocity and Rotation which Projectile Should Face
        var velocityRotation = originRotation * Quaternion.Euler(0f, 0f, spreadAngle);
        var facingRotation = velocityRotation * Quaternion.Euler(0f, 0f, -90f);

        // Instantiating New Projectile
        var newProjectile = Object.Instantiate(
            projectilePrefab,
            projectileSpawnPoint, 
            facingRotation);

        // Altering Projectile's Variables and Registering This Gun as it's Owner Gun
        var projectileBase = newProjectile.GetComponent<ProjectileEssentials>();
        projectileBase.CopyFrom(hitEssentials);
        if(ownerGun != null)
            projectileBase.ownerGun = ownerGun;

        // Apply Velocity
        Vector2 spreadDirection = velocityRotation * Vector2.right;
        var rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.velocity = spreadDirection * projectileSpeed;

        // Register New Projectile to the List if it Exists
        if (projectilesShot != null && !projectilesShot.Contains(newProjectile))
            projectilesShot.Add(newProjectile);
    }
}