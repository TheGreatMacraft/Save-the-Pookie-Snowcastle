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
            new AndConditions(
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
public sealed class StartHeavyAttackInputCondition : InputCondition
{
    public StartHeavyAttackInputCondition(InputActionStates allStates)
        : base(
            new AndConditions(
                new OnPressed(
                    allStates.PrimaryActionState()
                ),
                new OnBeingPressed(
                    allStates.PowerActionState()
                )
            )
        ) {}
}

public sealed class ChargeHeavyAttackInputCondition : InputCondition
{
    public ChargeHeavyAttackInputCondition(InputActionStates allStates)
        : base(
            new AndConditions(
                new OnBeingPressed(
                    allStates.PrimaryActionState()
                ),
                new OnBeingPressed(
                    allStates.PowerActionState()
                )
            )
        ) {}
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

// Toggle Build Menu
public sealed class BuildMenuInputCondition : InputCondition
{
    public BuildMenuInputCondition(InputActionStates allStates)
        : base(
            new OnPressed(allStates.SwitchStateActionState())
        ) {}
}

// Jump
public sealed class JumpInputCondition : InputCondition
{
    public JumpInputCondition(InputActionStates allStates)
        : base(
            new OnPressed(allStates.MovementSpecialActionState())
        ) {}
}