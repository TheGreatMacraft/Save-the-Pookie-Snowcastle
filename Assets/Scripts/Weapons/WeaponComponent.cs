using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public abstract class WeaponComponent
    : MonoBehaviour, Weapon
{
    [Header("Target")]
    [SerializeField] protected string targetTag;
    
    [Header("Camera Shake")]
    [SerializeField] private CameraShakeComponent cameraShakeComponent;
    [SerializeField] protected float shakeMagnitude;
    [SerializeField] protected float shakeDuration;
    
    [Header("Weapon Presentation")]
    [SerializeField] private SpriteRenderer weaponModel;
    
    
    private ActionExecution nullAction = new NullActionExecution();
    
    private Clock coroutineClock;
    private CameraShake cameraShake;
    private GameObject parent;
    
    private PhysicalBody weaponAnchor;
    private Location targetLocation;
    
    private PlayerMovement playerMovement;
    private PlayerState playerState;

    private Presentation weaponPresentation;


    protected Clock CoroutineClock()
        => coroutineClock ??=
            new CoroutineClock(this);

    protected CameraShake CameraShake()
        => cameraShake ??=
            (CameraShake)cameraShakeComponent ?? new NullCameraShake();
    
    private GameObject Parent()
        => parent ??= new ParentOfGameObject(gameObject).Value();


    private PhysicalBody WeaponAnchor()
        => weaponAnchor ??=
            new ComponentInObject<PhysicalBody>(
                gameObject,
                new NullPhysicalBody()
            ).Value();

    private Location TargetLocation()
        => targetLocation ??
           new ComponentInObject<TargetLocationSource>(
               new ParentOfGameObject(gameObject).Value(),
               new NullTargetLocationSource()
           ).Value();


    private PlayerMovement PlayerMovement()
        => playerMovement ??=
            new ComponentInObject<PlayerMovement>(
                Parent(),
                new NullPlayerMovement()
            ).Value();
    
    private PlayerState PlayerState()
        => playerState ??=
            new ComponentInObject<PlayerState>(
                Parent(),
                new NullPlayerState()
            ).Value();
    
    
    private Presentation WeaponPresentation()
        => weaponPresentation ??=
            new WeaponPresentation(
                weaponModel,
                WeaponAnchor(),
                TargetLocation(),
                PlayerMovement(),
                PlayerState()
            );


    public void Present() => weaponPresentation.Present();

    public virtual ActionExecution DefaultAttack() => nullAction;
    public virtual ActionExecution SupportAction() => nullAction;
    public virtual ActionExecution HeavyAttack() => nullAction;
    public virtual ActionExecution Ability() => nullAction;
}