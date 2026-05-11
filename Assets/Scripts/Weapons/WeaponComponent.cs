using System;
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
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
    private ActionExecution nullAction = new NullActionExecution();
    
    private Clock coroutineClock;
    private CameraShake cameraShake;
    private GameObject parent;
    
    private PhysicalBody weaponAnchor;
    private Location targetLocation;
    
    private PlayerMovement playerMovement;
    private PlayerState playerState;
    
    private Scalar<Weapon> selectedWeapon;
    private Condition isWeaponSelected;

    private Presentation weaponModel;


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


    private Scalar<Weapon> SelectedWeapon()
        => selectedWeapon ??=
            new ComponentInObject<Scalar<Weapon>>(
                Parent(),
                new NullScalar<Weapon>(new NullWeapon())
            ).Value();

    private Condition IsWeaponSelected()
        => isWeaponSelected ??=
            new IsSameCondition<Weapon>(SelectedWeapon(), this);
    
    
    private Presentation WeaponModel()
        => weaponModel ??=
            new WeaponModel(
                spriteRenderer,
                WeaponAnchor(),
                TargetLocation(),
                PlayerMovement(),
                PlayerState(),
                IsWeaponSelected()
            );


    private void Update()
    {
        WeaponModel().Present();
    }


    // Weapon Actions
    public virtual ActionExecution DefaultAttack() => nullAction;
    public virtual ActionExecution SupportAction() => nullAction;
    public virtual ActionExecution HeavyAttack() => nullAction;
    public virtual ActionExecution Ability() => nullAction;
}