using System.Collections.Generic;

public sealed class MultipleActionExecutions
    : ActionExecution
{
    private readonly ReadOnlyCollection<ActionExecution> actions;
    
    private List<Condition> inProgressCondition = new(1);
    private List<Condition> concludedCondition = new(1);


    public MultipleActionExecutions(params ActionExecution[] actions)
        : this(new SimpleReadOnlyCollection<ActionExecution>(actions)) {}
    
    public MultipleActionExecutions(ReadOnlyCollection<ActionExecution> actions)
    {
        this.actions = actions;
    }

    
    public void Execute()
    {
        foreach (ActionExecution action in actions.AllElements())
        {
            action.Execute();
        }
    }

    public Condition InProgress()
    {
        if (inProgressCondition.Count == 0)
        {
            inProgressCondition.Add(
                new IsTrue(() =>
                    {
                        foreach(ActionExecution action in actions.AllElements())
                            if(!action.InProgress().IsMet())
                                return false;
                        
                        return true;
                    }
                )
            );
        }
        
        return inProgressCondition[0];
    }
    
    public Condition Concluded()
    {
        if (concludedCondition.Count == 0)
            concludedCondition.Add(
                new IsTrue(() =>
                    {
                        foreach(ActionExecution action in actions.AllElements())
                            if(!action.Concluded().IsMet())
                                return false;
                        
                        return true;
                    }
                )
            );
        
        return concludedCondition[0];
    }
}