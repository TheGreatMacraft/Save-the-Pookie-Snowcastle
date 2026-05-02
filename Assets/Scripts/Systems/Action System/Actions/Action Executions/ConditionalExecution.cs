public sealed class ConditionalExecution
    : ActionExecution
{
    private readonly ActionExecution action;
    private readonly Condition condition;
    
    private Condition inProgressCondition;
    private Condition concludedCondition;


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
        => inProgressCondition ??=
            new OrConditions(
                action.InProgress(),
                condition
            );

    public Condition Concluded()
        => concludedCondition ??=
            new Not(InProgress());
}