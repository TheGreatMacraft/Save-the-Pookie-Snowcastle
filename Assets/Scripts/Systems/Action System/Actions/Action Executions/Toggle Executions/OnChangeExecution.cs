using System.Collections.Generic;
using UnityEngine;

public sealed class OnChangeExecution
    : ActionExecution
{
    private readonly ActionCall actionCall;
    private readonly Condition condition;
    
    private List<Condition> inProgressCondition = new(1);
    private List<Condition> concludedCondition = new(1);

    private bool lastState;

    public OnChangeExecution(
        ActionCall actionCall,
        Condition condition
    )
    {
        this.actionCall = actionCall;
        this.condition = condition;
    }


    public void Execute()
    {
        bool currentState = condition.IsMet();

        if (currentState != lastState)
        {
            actionCall.Call();
            lastState = currentState;
        }
    }

    public Condition InProgress()
    {
        if (inProgressCondition.Count == 0)
        {
            inProgressCondition.Add(
                new AndConditions(
                    condition,
                    new IsTrue(() => !lastState)
                )
            );
        }
        
        return inProgressCondition[0];
    }

    public Condition Concluded()
    {
        if (concludedCondition.Count == 0)
        {
            concludedCondition.Add(new Not(InProgress()));
        }
        
        return concludedCondition[0];
    }
}