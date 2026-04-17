using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public abstract class WeaponComponent :
    MonoBehaviour
{
    [Header("Target Tag")]
    [SerializeField] protected string targetTag;
    
    [Header("Player Movement")]
    [SerializeField] protected PlayerMovementComponent playerMovement;
    
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    
    [Header("Sprite Renderer")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    protected Clock coroutineClock;
    protected InputActionStates inputActionStates;
    
    private Orientation weaponOrientation;
    private Perspective weaponPerspective;
    
    
    protected virtual void Awake()
    {
        coroutineClock = new CoroutineClock(this);
        inputActionStates = new InputActionStates(playerInput);

        PhysicalBody weaponAnchor = new ComponentInObject<PhysicalBody>(
            gameObject,
            new NullPhysicalBody()
        ).Value();

        TargetLocationSource targetLocation = new ComponentInObject<TargetLocationSource>(
            new ParentOfGameObject(gameObject).Parent(),
            new NullTargetLocationSource()
        ).Value();
        
        Vector orientationVector = new Vector(
            new PointToPointVectorDefinition(
                weaponAnchor,
                targetLocation
            )
        );
        
        weaponOrientation = new WeaponOrientation(
            weaponAnchor,
            orientationVector
        );

        weaponPerspective = new WeaponPerspective(
            spriteRenderer,
            orientationVector
        );
    }

    private void Update()
    {
        weaponOrientation.Orient();
        weaponPerspective.SetDepth();
        
        defaultAttackAction.Execute();
        heavyAttackAction.Execute();
        supportAction.Execute();
        abilityAction.Execute();
    }
    
    
    protected ActionExecution defaultAttackAction = new NullActionExecution();
    protected ActionExecution heavyAttackAction = new NullActionExecution();
    protected ActionExecution supportAction = new NullActionExecution();
    protected ActionExecution abilityAction = new NullActionExecution();
}