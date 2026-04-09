using UnityEngine;

public sealed class RollCall 
    : ActionCall
{
    private readonly Force movement;
    private readonly Vector movementDirection;
    private readonly float rollForce;
    private readonly Animator playerAnimator;


    public RollCall(
        Force movement,
        Vector movementDirection,
        float rollForce,
        Animator playerAnimator
    )
    {
        this.movement = movement;
        this.movementDirection = movementDirection;
        this.rollForce = rollForce;
        this.playerAnimator = playerAnimator;
    }
    
    
    public void Call()
    {
        playerAnimator.SetTrigger("triggerRoll");
        movement.AddImpulse(movementDirection, rollForce);
    }
}