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
    
    
    protected Collection<Projectile> firedProjectiles
        = new SimpleCollection<Projectile>();

    private Magazine magazine;
    
    private ActionExecution reloadAction;
    private ActionExecution shootAction;


    protected Magazine Magazine()
        => magazine ??=
            infiniteMagazineSize
                ? new InfiniteMagazine()
                : new BasicMagazine(magazineSize);

    public override ActionExecution SupportAction()
        => reloadAction ??=
            new ExecutionWithCooldown(
                new ConstantExecution(new ReloadCall(Magazine())),
                reloadCooldown,
                CoroutineClock(),
                true
            );

    public override ActionExecution DefaultAttack()
        => shootAction ??=
            new ConditionalExecution(
                new ExecutionWithCooldown(
                    new ConstantExecution(
                        new ShootCall(
                            Magazine(),
                            ammoPerShot,
                            ammoSpreadAngle,
                            SupportAction(),
                            new StandardProjectileSpawner(
                                projectileSpeed,
                                projectileLifeTime,
                                new AlteringGameObjectBuilder<ProjectileComponent>
                                    (projectilePrefab),
                                firedProjectiles,
                                targetTag),
                            projectileSpawnPoint,
                            new Rotation(rotationAnchor),
                            CameraShake(),
                            shakeMagnitude,
                            shakeDuration
                        )
                    ),
                    shootCooldown,
                    CoroutineClock(),
                    false
                ),
                reloadAction.Concluded()
            );
}