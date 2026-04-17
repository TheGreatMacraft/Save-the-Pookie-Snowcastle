using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PhysicalBody))]
[RequireComponent(typeof(PhysicalMovement))]
[DisallowMultipleComponent]

public sealed class PlayerMovementComponent
    : MonoBehaviour, PlayerMovement
{
    [Header("Basic Movement")]
    [SerializeField] private float baseSpeed;
    
    private ComplexMovement playerLegs;
    
    
    [Header("Roll Properties")]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollDuration;
    [SerializeField] private float rollCooldown;
    
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    
    private ActionExecution rollAction;
    
    
    [Header("Other")]
    [SerializeField] private PlayerAnimationMessengerComponent playerAnimationMessenger;
    
    
    public ActionExecution RollAction()
        => rollAction;

    public Condition IsMoving()
        => playerLegs.IsMoving();
    
    public Condition RollConcluded()
        => rollAction.Concluded();
    
    
    private void Awake()
    {
        Clock coroutineClock = new CoroutineClock(this);
        
        Force playerMovement = new ComponentInObject<Force>(
            gameObject,
            new NullForce()
        ).Value();

        playerLegs = new InputAxisFollower(
            playerMovement,
            baseSpeed
        );

        rollAction = new InstantAction(
            new RollCall(
                playerMovement,
                new Vector(new InputAxisVectorDefinition()),
                rollForce,
                playerAnimationMessenger,
                coroutineClock,
                rollDuration
                ),
            rollCooldown,
            new MultipleConditions(
                new RollInputCondition(new InputActionStates(playerInput))
                ),
            coroutineClock
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