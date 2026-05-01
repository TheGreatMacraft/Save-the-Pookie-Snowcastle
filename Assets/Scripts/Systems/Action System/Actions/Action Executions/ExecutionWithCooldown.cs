using System.Collections.Generic;

public sealed class ExecutionWithCooldown
    : ActionExecution
{
    private readonly ActionExecution execution;
    private readonly float cooldown;
    private readonly Clock coroutineClock;
    private readonly bool executeAfterCooldown;

    private bool inProgress = false;
    private List<Condition> inProgressCondition = new(1);
    private List<Condition> concludedCondition = new(1);

    
    public ExecutionWithCooldown(
        ActionExecution execution,
        float cooldown,
        Clock coroutineClock,
        bool executeAfterCooldown
    )
    {
        this.execution = execution;
        this.cooldown = cooldown;
        this.coroutineClock = coroutineClock;
        this.executeAfterCooldown = executeAfterCooldown;
    }


    public void Execute()
    {
        if(inProgress) return;
        
        inProgress = true;
        
        if(!executeAfterCooldown)
            execution.Execute();
        
        coroutineClock.Schedule(() =>
            {
                if(executeAfterCooldown)
                    execution.Execute();
                inProgress = false;
            }
            , cooldown);
    }

    public Condition InProgress()
    {
        if (inProgressCondition.Count == 0)
        {
            inProgressCondition.Add(
                new IsTrue(() => inProgress)
            );
        }
        
        return inProgressCondition[0];
    }

    public Condition Concluded()
    {
        if (concludedCondition.Count == 0)
            concludedCondition.Add(new Not(InProgress()));
        
        return concludedCondition[0];
    }
}