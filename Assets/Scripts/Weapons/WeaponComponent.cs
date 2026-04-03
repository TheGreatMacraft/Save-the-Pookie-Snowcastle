using UnityEngine;
using UnityEngine.InputSystem;

public abstract class WeaponComponent :
    MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] protected PlayerInput playerInput;
    
    [Header("Target Tag")]
    [SerializeField] protected string targetTag;
    
    protected Clock coroutineClock;
    protected InputActionState inputActionState;
    private ActionInterpreter abilityInterpreter;
    
    protected ActionExecution abilityAction;
    protected readonly ActionExecution nullActionExecution 
        = new NullActionExecution();
    
    
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
                    new AbilityInputAction()
                    )
                )
        );
    }

    protected virtual void Update()
    {
        abilityInterpreter.ExecuteActionCall();
    }
}