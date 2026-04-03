using UnityEngine;
using UnityEngine.PlayerLoop;

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

    private ActionInterpreter gunInterpreter;

    protected Collection<Projectile> firedProjectiles
        = new SimpleCollection<Projectile>();
    
    private ActionExecution shootAction;
    private ActionExecution reloadAction;
    
    protected virtual void Awake()
    {
        base.Awake();
        
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

        gunInterpreter = new AllActionsInterpreter(
            new SimpleReadOnlyCollection<ActionInterpreter>(
                
                new InputActionLink(
                    shootAction,
                    new OnPressed(
                        new InputActionState(
                            playerInput,
                            new PrimaryInputAction()
                            )
                        )
                    ),
                
                new InputActionLink(
                    reloadAction,
                    new OnPressed(
                        new InputActionState(
                            playerInput,
                            new SecondaryInputAction()
                            )
                        )
                    )
                )
        );
    }

    protected override void Update()
    {
        base.Update();
        
        gunInterpreter.ExecuteActionCall();
    }
}