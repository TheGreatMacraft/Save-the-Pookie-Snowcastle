using UnityEngine;
[DisallowMultipleComponent]


public abstract class GunComponent
    : WeaponComponent
{
    [Header("Magazine")]
    [SerializeField] private bool infiniteMagazineSize;
    [SerializeField] private int magazineSize;
    
    [Header("Reload")]
    [SerializeField] private float reloadCooldown;
    
    [Header("Shoot")]
    [SerializeField] private float shootCooldown;
    [SerializeField] private int ammoPerShot;
    [SerializeField] private float ammoSpreadAngle;
    [SerializeField] private PhysicalBodyComponent rotationAnchor;

    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private PhysicalBodyComponent projectileSpawnPoint;
    [SerializeField] private ProjectileComponent projectilePrefab;
    
    
    protected Magazine magazine; 
    
    protected Collection<Projectile> firedProjectiles
        = new SimpleCollection<Projectile>();
    

    protected override void Awake()
    {
        base.Awake();
        
        magazine = infiniteMagazineSize
            ? new InfiniteMagazine()
            : new BasicMagazine(magazineSize);
    }
    
    protected virtual void Start()
    {
        ActionExecution reloadAction = new ExecutionWithCooldown(
            new ConstantExecution(new ReloadCall(magazine)),
            reloadCooldown,
            coroutineClock,
            true
        );
        
        ActionExecution shootAction = new ExecutionWithCooldown(
            new ConstantExecution(
                new ShootCall(
                    magazine,
                    ammoPerShot,
                    ammoSpreadAngle,
                    reloadAction,
                    new StandardProjectileSpawner(
                        projectileSpeed,
                        projectileLifeTime,
                        new GameObjectBuilder<ProjectileComponent>
                            (projectilePrefab),
                        firedProjectiles,
                        targetTag),
                    projectileSpawnPoint,
                    new Rotation(rotationAnchor),
                    cameraShake,
                    shakeMagnitude,
                    shakeDuration
                )
            ),
            shootCooldown,
            coroutineClock,
            false
        );

        defaultAttack = shootAction;
        supportAction = reloadAction;
    }
}