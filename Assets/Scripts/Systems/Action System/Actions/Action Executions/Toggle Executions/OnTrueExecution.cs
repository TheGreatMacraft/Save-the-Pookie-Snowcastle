using System.Collections.Generic;

public sealed class OnTrueExecution
    : ActionExecution
{
    private readonly ActionCall actionCall;
    private readonly Condition condition;

    private bool lastState;
    private List<Condition> inProgressCondition = new(1);
    private List<Condition> concludedCondition = new(1);

    public OnTrueExecution(
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
            if(currentState)
                actionCall.Call();
            lastState = currentState;
        }
        
    }
    
    public Condition InProgress()
    {
        if (inProgressCondition.Count == 0)
        {
            inProgressCondition.Add(
                new MultipleConditions(
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