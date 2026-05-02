using System.Collections.Generic;
using UnityEngine;

public sealed class OnChangeExecution
    : ActionExecution
{
    private readonly ActionExecution action;
    private readonly Condition condition;
    
    private Condition inProgressCondition;
    private Condition concludedCondition;

    private bool lastState;

    
    public OnChangeExecution(
        ActionExecution action,
        Condition condition
    )
    {
        this.action = action;
        this.condition = condition;
    }


    public void Execute()
    {
        bool currentState = condition.IsMet();

        if (currentState != lastState)
        {
            action.Execute();
            lastState = currentState;
        }
    }

    public Condition InProgress()
        => inProgressCondition ??=
            new AndConditions(
                condition,
                new IsTrue(() => !lastState)
            );

    public Condition Concluded()
        => concludedCondition ??= new Not(InProgress());
}