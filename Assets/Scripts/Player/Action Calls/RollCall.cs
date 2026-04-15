using UnityEngine;

public sealed class RollCall 
    : ActionCall
{
    private readonly Force movement;
    private readonly Vector movementDirection;
    private readonly float rollForce;
    
    private readonly AnimatedRoll animator;
    
    private readonly Clock coroutineClock;
    private readonly float rollDuration;


    public RollCall(
        Force movement,
        Vector movementDirection,
        float rollForce,
        AnimatedRoll animator,
        Clock coroutineClock,
        float rollDuration
    )
    {
        this.movement = movement;
        this.movementDirection = movementDirection;
        this.rollForce = rollForce;
        this.animator = animator;
        this.coroutineClock = coroutineClock;
        this.rollDuration = rollDuration;
    }
    
    
    public void Call()
    {
        Vector currentMovementDirection = new Vector(
            new ConstantVectorDefinition(
                movementDirection.RawVector()
            )
        );
        
        movement.SetForce(currentMovementDirection, rollForce);
        
        coroutineClock.DoUntil((progression) =>
            {
                if (progression < 0.1)
                    progression = 0.1f;
                float currentSpeed = Mathf.Lerp(rollForce, 0f, progression);
                movement.SetForce(currentMovementDirection, currentSpeed);
            },
            rollDuration,
            () =>
            {
                //movement.ResetForce();
            }
            );
        
        animator.TriggerRolling();
    }
}