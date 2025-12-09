using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GunBaseClass : WeaponBaseClass
{
    // Number of Projectiles
    public int projectilesBeforeReload;
    public int projectilesPerShoot;

    // Time
    public float reloadTime;
    public float timeBetweenShots;

    // Bullet
    public int projectileDamage;
    public float projectileSpeed;

    // Spread
    public float spread;

    // Camera Shake
    public float cameraShakeMagnitude;
    public float cameraShakeDuration;

    // Values To Be Assigned in Inspector
    public GameObject projectilePrefab;
    public GameObject gunRotationAnchor;
    public GameObject projectileSpawnPoint;

    // Values Used in Script
    Vector2 shootDirection;

    int currentProjectileCount;
    bool isReloading;
    bool canShoot = true;

    // Projectile Tracker
    [System.NonSerialized]
    public List<GameObject> projectilesShot = new List<GameObject>();


    public override void Start()
    {
        base.Start();

        // Set Ammo in Magazine to Max Allowed
        currentProjectileCount = projectilesBeforeReload;
    }

    public override void AttackCall(InputAction.CallbackContext context)
    {
        base.AttackCall(context);

        if (currentProjectileCount == 0) { return; }

        if(!canShoot) {  return; }

        // Spawn Projectiles
        for(int i=0; i < projectilesPerShoot; i++)
            SpawnProjectile();

        // Reduce Ammunition in Magazine
        currentProjectileCount -= projectilesPerShoot;

        // Start Cooldown
        canShoot = false;
        StartCoroutine(CallActionAfterTime(timeBetweenShots,() => canShoot = true));

        // Camera Shake
        StartCoroutine(CameraShake.instance.ShakeCamera(cameraShakeMagnitude, cameraShakeDuration));
    }

    public void SpawnProjectile()
    {
        // Calculate Angle with Spread
        float spreadAngle = UnityEngine.Random.Range(-spread, spread);

        // Calculate Rotation for Applied Velocity and Rotation which Projectile Should Face
        Quaternion velocityRotation = gunRotationAnchor.transform.rotation * Quaternion.Euler(0f, 0f, spreadAngle);
        Quaternion facingRotation = velocityRotation * Quaternion.Euler(0f, 0f, -90f);

        // Instantiating New Projectile
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, facingRotation);
        
        // Altering Projectile's Damage and Registering This Gun as it's Owner Gun
        ProjectileBaseClass projectileBaseClass = newProjectile.GetComponent<ProjectileBaseClass>();
        projectileBaseClass.damage = projectileDamage;
        projectileBaseClass.ownerGun = this;

        // Apply Velocity
        Vector2 spreadDirection = velocityRotation * Vector2.right;
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.velocity = spreadDirection * projectileSpeed;

        // Register New Projectile to the List
        if (!projectilesShot.Contains(newProjectile))
            projectilesShot.Add(newProjectile);
    }  

    public void ReloadCall(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        if (currentProjectileCount == projectilesBeforeReload) { return; }

        if(isReloading) { return; }

        // Call Reload after Reload Time
        isReloading = true;
        StartCoroutine(CallActionAfterTime(reloadTime,Reload));
    }

    public void Reload()
    {
        // Set current ammo to max ammo
        currentProjectileCount = projectilesBeforeReload;
        isReloading = false;
    }

    // Coroutine that calls Parameter Method after Given Time
    public IEnumerator CallActionAfterTime(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action();
    }

    public override void AbilityCall(InputAction.CallbackContext context)
    {
        base.AbilityCall(context);

        Ability();
    }

    // Virtual Method Ability, to be Defined in Derived Class
    public virtual void Ability() { }

    // Linking Base Weapon Buttons + Reload Button
    public override void SetButtonFunctions()
    {
        base.SetButtonFunctions();

        var reloadAction = playerInput.actions["ReloadButton"];

        reloadAction.performed += ReloadCall;
        reloadAction.Enable();

    }
}