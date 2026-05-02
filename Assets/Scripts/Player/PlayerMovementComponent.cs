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
    
    
    private Clock coroutineClock;
    private Force playerForce;

    private SpeedProvider speedProvider;
    private Speed speed;
    private ComplexMovement legs;
    
    private ActionExecution rollAction;
    private ActionExecution playerMovement;


    private Clock CoroutineClock()
        => coroutineClock ??=
            new CoroutineClock(this);

    private Force PlayerForce()
        => playerForce ??=
            new ComponentInObject<Force>(
                gameObject,
                new NullForce()
            ).Value();

    private SpeedProvider SpeedProvider()
        => speedProvider ??=
            new ComponentInObject<SpeedProvider>(
                gameObject,
                new NullSpeedProvider()
            ).Value();
    
    private Speed Speed()
        => speed ??=
            new ComplexSpeed(
                SpeedProvider().DefaultSpeedMultiplier(),
                new AttributeModifier(
                    SpeedProvider().SprintSpeedMultiplier(),
                    new FalseCondition()
                )
            );
    
    private ComplexMovement Legs()
        => legs ??=
            new InputAxisFollower(
                new ComponentInObject<Force>(
                    gameObject,
                    new NullForce()
                ).Value(),
                Speed()
            );
    
    
    public ActionExecution RollAction()
        => rollAction ??=
            new ConditionalExecution(
                new ExecutionWithCooldown(
                    new ConstantExecution(
                        new RollCall(
                            PlayerForce(),
                            new Vector(new InputAxisVectorDefinition()),
                            rollForce,
                            CoroutineClock(),
                            rollDuration
                        )
                    ),
                    rollCooldown,
                    CoroutineClock(),
                    false
                ),
                new RollInputCondition(new InputActionStates(playerInput))
            );
    

    public ActionExecution Movement()
        => playerMovement ??=
            new ConditionalExecution(
                new ConstantExecution(
                    new SimpleActionCall(() => Legs().Move())
                ),
                new AndConditions(
                    RollAction().Concluded(),
                    Legs().IsMoving()
                )
            );


    private void Update()
    {
        RollAction().Execute();
    }

    private void FixedUpdate()
    {
        Movement().Execute();
    }
}