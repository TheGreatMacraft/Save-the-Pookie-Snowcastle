public sealed class OnTrueExecution
    : ActionExecution
{
    private readonly ActionExecution action;
    private readonly Condition condition;

    private bool lastState;
    
    private Condition inProgressCondition;
    private Condition concludedCondition;

    
    public OnTrueExecution(
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
            if(currentState)
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