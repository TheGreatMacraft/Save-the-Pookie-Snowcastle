using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PhysicalMovement))]
[DisallowMultipleComponent]

public sealed class PlayerMovement
    : MonoBehaviour
{
    [Header("Basic Movement")]
    [SerializeField] private float baseSpeed;
    
    [Header("Roll Properties")]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollCooldown;
    
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator playerAnimator;

    private Vector movementDirection;
    private ActionInterpreter allMoves;
    private Movement legs;

    private ActionExecution rollAction;
    
    private void Awake()
    {
        Clock coroutineClock = new CoroutineClock(this);
        
        Force playerMovement = new ComponentInObject<Force>(
            gameObject,
            new NullForce()
        ).Value();
        
        movementDirection = new Vector(
            new InputAxisVectorDefinition()
        );
        
        legs = new Legs(
            playerMovement,
            movementDirection,
            baseSpeed
        );

        rollAction = new InstantAction(
            new RollCall(
                playerMovement,
                movementDirection,
                rollForce,
                playerAnimator
                ),
            rollCooldown,
            coroutineClock
        );
        
        allMoves = new AllActionsInterpreter(
            new SimpleReadOnlyCollection<ActionInterpreter>(
                
                new InputActionLink(
                    rollAction,
                    new OnPressed(
                        new InputActionState(
                            playerInput,
                            new  RollInputAction()
                            )
                        )
                    )
                )
        );
    }
    
    private void FixedUpdate()
    {
        playerAnimator.SetBool("isRunning",
            movementDirection.RawVector() != Vector3.zero
            );
        
        if(rollAction.Concluded())
            legs.Move();
        
        allMoves.ExecuteActionCall();
    }
}