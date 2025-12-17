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

    // Variables to be Assigned in Inspector
    public GameObject projectilePrefab;
    public GameObject gunRotationAnchor;
    public GameObject projectileSpawnPoint;

    private int currentProjectileCount;

    // Projectile Tracker
    [NonSerialized] public List<GameObject> projectilesShot = new();

    // Variables used in Script
    private Vector2 shootDirection;


    protected override void Awake()
    {
        base.Awake();
        
        // Set Ammo in Magazine to Max Allowed
        currentProjectileCount = projectilesBeforeReload;
    }

    public void Attack()
    {
        // Cancel if no Ammo in Magazine or Is Reloading
        if (currentProjectileCount == 0 || !actionModules["Reload"].canAct) return;
        
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
    }

    public void Reload()
    {
        // Cancel if Magazine is Full
        if (currentProjectileCount == projectilesBeforeReload) return;

        // Set current ammo to max ammo
        currentProjectileCount = projectilesBeforeReload;
    }

    public bool NeedsReloading()
    {
        return currentProjectileCount == 0;
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