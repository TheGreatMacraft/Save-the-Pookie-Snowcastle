using UnityEngine;

public abstract class InputCondition 
    : Condition
{
    private readonly Condition condition;

    protected InputCondition(Condition condition)
    {
        this.condition = condition;
    }

    public bool IsMet() 
        => condition.IsMet();
}

// Default Attack
public sealed class DefaultAttackInputCondition : InputCondition
{
    public DefaultAttackInputCondition(InputActionStates allStates)
        : base(
            new MultipleConditions(
                new OnPressed(
                    allStates.PrimaryActionState()
                ),
                new OnBeingReleased(
                    allStates.PowerActionState()
                )
            )
        ) {}
}

// Heavy Attack
public sealed class HeavyAttackInputCondition : Condition
{
    private readonly Condition start;
    private readonly Condition running;
    private bool isHolding;

    public HeavyAttackInputCondition(InputActionStates states)
    {
        start = new MultipleConditions
        (
            new OnPressed(states.PrimaryActionState()),
            new OnBeingPressed(states.PowerActionState())
        );
        
        running = new MultipleConditions
        (
            new OnBeingPressed(states.PrimaryActionState()),
            new OnBeingPressed(states.PowerActionState())
        );
    }

    public bool IsMet()
    {
        if (!isHolding)
        {
            if (start.IsMet())
            {
                isHolding = true;
            }
            return isHolding;
        }
        
        isHolding = running.IsMet();
        
        return isHolding;
    }
}

// Support Action
public sealed class SupportActionInputCondition : InputCondition
{
    public SupportActionInputCondition(InputActionStates allStates)
        : base(
            new OnPressed(allStates.SupportActionState())
        ) {}
}

// Secondary Action
public sealed class SecondaryInputCondition : InputCondition
{
    public SecondaryInputCondition(InputActionStates allStates)
        : base(
            new OnPressed(allStates.SecondaryActionState())
        ) {}
}

// Ability
public sealed class AbilityInputCondition : InputCondition
{
    public AbilityInputCondition(InputActionStates allStates)
        : base(
            new OnPressed(allStates.SpecialActionState())
        ) {}
}

// Roll
public sealed class RollInputCondition : InputCondition
{
    public RollInputCondition(InputActionStates allStates)
        : base(
            new OnPressed(allStates.MovementActionState())
        ) {}
}