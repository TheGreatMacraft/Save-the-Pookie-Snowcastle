using System.Collections.Generic;

public sealed class ConditionalExecution
    : ActionExecution
{
    private readonly ActionExecution action;
    private readonly Condition condition;
    
    private List<Condition> inProgressCondition = new(1);
    private List<Condition> concludedCondition = new(1);


    public ConditionalExecution(
        ActionExecution action,
        Condition condition
    )
    {
        this.action = action;
        this.condition = condition;
    }


    public void Execute()
    {
        if(condition.IsMet())
            action.Execute();
    }

    public Condition InProgress()
    {
        if (inProgressCondition.Count == 0)
           inProgressCondition.Add(action.InProgress());
        
        return inProgressCondition[0];
    }

    public Condition Concluded()
    {
        if (concludedCondition.Count == 0)
            concludedCondition.Add(new Not(InProgress()));
        
        return concludedCondition[0];
    }
}