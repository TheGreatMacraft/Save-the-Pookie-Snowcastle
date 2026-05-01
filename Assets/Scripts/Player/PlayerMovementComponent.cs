using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PhysicalBody))]
[RequireComponent(typeof(PhysicalMovement))]
[DisallowMultipleComponent]

public sealed class PlayerMovementComponent
    : MonoBehaviour, PlayerMovement
{
    [Header("Roll Properties")]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollDuration;
    [SerializeField] private float rollCooldown;
    
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    
    private ActionExecution rollAction;
    
    
    [Header("Other")]
    [SerializeField] private PlayerAnimationMessengerComponent playerAnimationMessenger;
    
    
    private ComplexMovement playerLegs;
    
    public ActionExecution RollAction()
        => rollAction;

    public Condition IsMoving()
        => playerLegs.IsMoving();
    
    
    private void Awake()
    {
        Clock coroutineClock = new CoroutineClock(this);
        
        SpeedProvider speedProvider = new ComponentInObject<SpeedProvider>(
            gameObject,
            new NullSpeedProvider()
        ).Value();
        
        Speed playerSpeed = new ComplexSpeed(
            speedProvider.DefaultSpeedMultiplier(),
            new AttributeModifier(
                speedProvider.SprintSpeedMultiplier(),
                new FalseCondition()
                )
        );
        
        Force playerMovement = new ComponentInObject<Force>(
            gameObject,
            new NullForce()
        ).Value();

        playerLegs = new InputAxisFollower(
            playerMovement,
            playerSpeed
        );

        rollAction = new ConditionalExecution(
            new ExecutionWithCooldown(
                new ConstantExecution(
                    new RollCall(
                        playerMovement,
                        new Vector(new InputAxisVectorDefinition()),
                        rollForce,
                        playerAnimationMessenger,
                        coroutineClock,
                        rollDuration
                    )
                ),
                rollCooldown,
                coroutineClock,
                false
            ),
            new RollInputCondition(new InputActionStates(playerInput))
        );
    }


    private void Update()
    {
        rollAction.Execute();
    }

    private void FixedUpdate()
    {
        playerAnimationMessenger.ToggleWalking(playerLegs.IsMoving().IsMet());
        
        if(rollAction.Concluded().IsMet())
            playerLegs.Move();
    }
}