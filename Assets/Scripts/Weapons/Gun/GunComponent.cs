using System;
using UnityEngine;

public abstract class GunComponent : 
    WeaponComponent
{
    [Header("Shooting Properties")]
    [SerializeField] private float shootCooldown;
    [SerializeField] private int magazineSize;
    [SerializeField] private int ammoPerShot;
    [SerializeField] private PhysicalBodyComponent rotationAnchor;
    
    [Header("Reload Properties")]
    [SerializeField] private float reloadCooldown;

    [Header("Projectile Properties")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private PhysicalBodyComponent projectileSpawnPoint;
    [SerializeField] private ProjectileComponent projectilePrefab;
    
    [Header("Camera Shake Properties")]
    [SerializeField] private CameraShakeComponent cameraShake;
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeDuration;

    protected Collection<Projectile> firedProjectiles
        = new SimpleCollection<Projectile>();

    private void Start()
    {
        Magazine magazine = new BasicMagazine(magazineSize);
        
        defaultAttackAction = new InstantAction(
            new ShootCall(
                magazine,
                ammoPerShot,
                new StandardProjectileSpawner(
                    projectileSpeed,
                    new GameObjectBuilder<ProjectileComponent>(projectilePrefab),
                    firedProjectiles,
                    targetTag),
                projectileSpawnPoint,
                new Rotation(rotationAnchor),
                cameraShake,
                shakeMagnitude,
                shakeDuration
            ),
            shootCooldown,
            new MultipleConditions(
                new DefaultAttackInputCondition(inputActionStates),
                playerMovement.RollConcluded()
            ),
            coroutineClock
        );
            
        supportAction = new DelayedAction(
            new ReloadCall(magazine),
            reloadCooldown,
            new SupportActionInputCondition(inputActionStates),
            coroutineClock
        );

        heavyAttackAction = new ChargedActionExecution(
            new NullActionCall(),
            new NullActionExecution(),
            2f,
            coroutineClock,
            new HeavyAttackInputCondition(inputActionStates)
        );
    }
}