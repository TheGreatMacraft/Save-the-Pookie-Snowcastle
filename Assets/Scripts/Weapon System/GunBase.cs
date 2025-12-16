using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : WeaponBase
{
    // Number of Projectiles
    public int projectilesBeforeReload;
    public int projectilesPerShoot;

    // Bullet
    public float projectileSpeed;

    // Spread
    public float spread;

    // Camera Shake
    public float cameraShakeMagnitude;
    public float cameraShakeDuration;

    // Variables to be Assigned in Inspector
    public GameObject projectilePrefab;
    public GameObject gunRotationAnchor;
    public GameObject projectileSpawnPoint;

    private int currentProjectileCount;

    // Projectile Tracker
    [NonSerialized] public List<GameObject> projectilesShot = new();

    // Variables used in Script
    private Vector2 shootDirection;


    private void Awake()
    {
        // Set Ammo in Magazine to Max Allowed
        currentProjectileCount = projectilesBeforeReload;
    }

    public void Attack()
    {
        // Cancel if no Ammo in Magazine
        if (currentProjectileCount == 0) return;
        
        for (var i = 0; i < projectilesPerShoot; i++)
            ShootingComponent.SpawnProjectile(
                projectilePrefab, 
                gunRotationAnchor,
                projectileSpawnPoint,
                projectileSpeed,
                spread,
                this,
                projectilesShot
            );
        
        // Reduce Ammunition in Magazine
        currentProjectileCount -= projectilesPerShoot;
        
        // Camera Shake
        StartCoroutine(CameraShake.instance.ShakeCamera(cameraShakeMagnitude, cameraShakeDuration));
    }

    public void Reload()
    {
        // Cancel if Magazine is Full
        if (currentProjectileCount == projectilesBeforeReload) return;

        // Set current ammo to max ammo
        currentProjectileCount = projectilesBeforeReload;
    }

    protected override Dictionary<string, Action> WeaponActionFunctions()
    {
        return new Dictionary<string, Action>
        {
            { "Attack", Attack },
            { "Reload", Reload },
        };
    }
}