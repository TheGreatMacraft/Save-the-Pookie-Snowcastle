using System;
using UnityEngine;
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
    
    private ActionExecution rollAction;
    
    
    [Header("Other")]
    [SerializeField] private PlayerAnimationMessengerComponent playerAnimationMessenger;
    
    
    public ActionExecution RollAction()
        => rollAction;

    public bool IsMoving()
        => playerLegs.isMoving();
    
    
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
            coroutineClock
        );
    }
    
    private void FixedUpdate()
    {
        playerAnimationMessenger.ToggleWalking(playerLegs.isMoving());
        
        if(rollAction.Concluded())
            playerLegs.Move();
    }
}