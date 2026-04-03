public sealed class AllActionsInterpreter : 
    ActionInterpreter
{
    private readonly ReadOnlyCollection<ActionInterpreter> actions;
    
    
    public AllActionsInterpreter(
        ReadOnlyCollection<ActionInterpreter> actions
    )
    {
        this.actions = actions;
    }

    
    public void ExecuteActionCall()
    {
        foreach (ActionInterpreter action in actions.AllElements())
        {
            action.ExecuteActionCall();
        }
    }
}