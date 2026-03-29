using UnityEngine;
using UnityEngine.InputSystem;

public abstract class WeaponComponent :
    MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private PlayerInput playerInput;
    
    [Header("Target Tag")]
    [SerializeField] protected string targetTag;
    
    protected Clock coroutineClock;
    protected InputSystem inputSystem;
    protected ActionInterpreter actionInterpreter;
    
    protected ActionExecution abilityAction;
    protected readonly ActionExecution nullActionExecution 
        = new NullActionExecution();
    
    
    protected virtual ActionExecution AddAbility() 
        => new NullActionExecution();

    
    protected virtual void Awake()
    {
        coroutineClock = new CoroutineClock(this);
        inputSystem = new InputSystem(playerInput);
        
        abilityAction = AddAbility();
    }

    private void Update()
    {
        actionInterpreter.ExecuteActionCalls();
    }
}