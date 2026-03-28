using UnityEngine;
using UnityEngine.InputSystem;
[DisallowMultipleComponent]

public abstract class GunComponent : 
    MonoBehaviour
{
    [Header("Shooting Properties")]
    [SerializeField] private float shootCooldown;
    [SerializeField] private int magazineSize;
    [SerializeField] private int ammoPerShot;
    [SerializeField] private PhysicalBodyComponent rotationAnchor;
    [SerializeField] protected string targetTag;
    
    [Header("Reload Properties")]
    [SerializeField] private float reloadCooldown;

    [Header("Projectile Properties")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private PhysicalBodyComponent projectileSpawnPoint;
    [SerializeField] private ProjectileComponent projectilePrefab;
    
    [Header("Camera")]
    [SerializeField] private Camera camera;
    
    [Header("Input System")]
    [SerializeField] private PlayerInput playerInput;
    
    
    protected Clock coroutineClock;
    private InputSystem inputSystem;
    private ActionInterpreter actionInterpreter;
    
    private Location mouseWorldLocation;
    private Location rotationAnchorLocationSource;
    private Rotatable weaponRotation;
        
    protected Collection<Projectile> firedProjectiles;
    
    private ActionExecution shootAction;
    private ActionExecution reloadAction;
    private ActionExecution abilityAction;

    private Orientation weaponOrientation;

    protected virtual ActionExecution AddAbility() => new NullActionExecution();
    
    protected virtual void Awake()
    {
        // Coroutine Clock & Input System
        coroutineClock = new CoroutineClock(this);
        inputSystem = new InputSystem(playerInput);
        
        // Rotate to Face Mouse
        rotationAnchorLocationSource = rotationAnchor;
        weaponRotation = rotationAnchor;

        mouseWorldLocation = new ScreenPositionAsWorldPosition(
            camera,
            new MouseCursorScreenPosition()
        );
        
        weaponOrientation = new WeaponOrientation(
            weaponRotation,
            new Vector(
                new PointToPointVectorDefinition(
                    rotationAnchorLocationSource,
                    mouseWorldLocation
                    )
                )
        );
            
        // Shooting, Reloading & ProjectileCollection
        firedProjectiles = new FiredProjectiles();
        
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

        abilityAction = AddAbility();

        actionInterpreter = new InputInterpreter(
            shootAction,
            reloadAction,
            abilityAction,
            inputSystem
            );
    }

    private void Update()
    {
        weaponOrientation.Orient();
        actionInterpreter.ExecuteActionCalls();
    }
}