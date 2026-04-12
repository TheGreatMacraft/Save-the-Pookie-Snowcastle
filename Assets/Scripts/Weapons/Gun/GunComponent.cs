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

    protected Collection<Projectile> firedProjectiles
        = new SimpleCollection<Projectile>();
    
    
    protected virtual void Awake()
    {
        base.Awake();
        
        Magazine magazine = new BasicMagazine(magazineSize);
            
        primaryAction = new InstantAction(
            new ShootCall(
                magazine,
                ammoPerShot,
                new StandardProjectileSpawner(
                    projectileSpeed,
                    new GameObjectBuilder<ProjectileComponent>(projectilePrefab),
                    firedProjectiles,
                    targetTag),
                projectileSpawnPoint,
                new Rotation(rotationAnchor)
                ),
            shootCooldown,
            coroutineClock
        );
            
        supportAction = new DelayedAction(
            new ReloadCall(magazine),
            reloadCooldown,
            coroutineClock
        );
    }
}