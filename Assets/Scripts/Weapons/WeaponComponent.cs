using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public abstract class WeaponComponent :
    MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] protected PlayerInput playerInput;
    
    [Header("Target Tag")]
    [SerializeField] protected string targetTag;
    
    protected Clock coroutineClock;
    private ActionInterpreter abilityInterpreter;
    
    protected ActionExecution abilityAction;
    protected readonly ActionExecution nullActionExecution 
        = new NullActionExecution();

    private Orientation weaponOrientation;
    
    protected virtual ActionExecution AddAbility() 
        => new NullActionExecution();
    
    
    protected virtual void Awake()
    {
        coroutineClock = new CoroutineClock(this);
        abilityAction = AddAbility();
        
        abilityInterpreter = new InputActionLink(
            abilityAction,
            new OnPressed(
                new InputActionState(
                    playerInput,
                    new SpecialInputAction()
                    )
                )
        );

        weaponOrientation = new WeaponOrientation(
            new ComponentInObject<PhysicalBody>(
                gameObject,
                new NullPhysicalBody()
                ).Value(),
            new ComponentInObject<TargetLocationSource>(
                new ParentOfGameObject(gameObject).Parent(),
                new NullTargetLocationSource()
                ).Value()
        );
    }

    protected virtual void Update()
    {
        weaponOrientation.Orient();
        abilityInterpreter.ExecuteActionCall();
    }
}