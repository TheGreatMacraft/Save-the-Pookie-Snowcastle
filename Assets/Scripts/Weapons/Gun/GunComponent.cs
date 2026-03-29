using UnityEngine;
[RequireComponent(typeof(WeaponOrientationComponent))]
[DisallowMultipleComponent]

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
        
    
    protected Collection<Projectile> firedProjectiles;
    
    private ActionExecution shootAction;
    private ActionExecution reloadAction;
    
    protected virtual void Awake()
    {
        base.Awake();
        
        
        firedProjectiles = new SimpleCollection<Projectile>();
        Magazine magazine = new BasicMagazine(magazineSize);
            
        shootAction = new InstantAction(
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
            
        reloadAction = new DelayedAction(
            new ReloadCall(magazine),
            reloadCooldown,
            coroutineClock
        );

        actionInterpreter = new InputInterpreter(
            shootAction,
            reloadAction,
            abilityAction,
            inputSystem
            );
    }
}