using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
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
        GameObject gunRotationAnchor,
        GameObject projectileSpawnPoint,
        float projectileSpeed,
        [CanBeNull] float? spread = null,
        [CanBeNull] GunBase ownerGunScript = null,
        [CanBeNull] List<GameObject> projectilesShot = null)
    {
        
        // Calculate Angle with Spread
        float spreadAngle = spread.HasValue ? Random.Range(-spread.Value, spread.Value) : 0;

        // Calculate Rotation for Applied Velocity and Rotation which Projectile Should Face
        var velocityRotation = gunRotationAnchor.transform.rotation * Quaternion.Euler(0f, 0f, spreadAngle);
        var facingRotation = velocityRotation * Quaternion.Euler(0f, 0f, -90f);

        // Instantiating New Projectile
        var newProjectile = Object.Instantiate(
            projectilePrefab,
            projectileSpawnPoint.transform.position, 
            facingRotation);

        // Altering Projectile's Variables and Registering This Gun as it's Owner Gun
        var projectileBase = newProjectile.GetComponent<ProjectileBase>();
        projectileBase.CopyFrom(ownerGunScript);
        projectileBase.ownerGun = ownerGunScript;

        // Apply Velocity
        Vector2 spreadDirection = velocityRotation * Vector2.right;
        var rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.velocity = spreadDirection * projectileSpeed;

        // Register New Projectile to the List if it Exists
        if (projectilesShot != null && !projectilesShot.Contains(newProjectile))
            projectilesShot.Add(newProjectile);
    }
}