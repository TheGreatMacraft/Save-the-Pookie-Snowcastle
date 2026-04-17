public sealed class InstantAction 
    : ActionExecution
{
    private readonly ActionCall actionCall;
    private readonly float cooldown;
    private readonly Condition condition;
    private readonly Clock coroutineClock;

    private bool canAct = true;


    public InstantAction(
        ActionCall actionCall,
        float cooldown,
        Condition condition,
        Clock coroutineClock
        )
    {
        this.actionCall = actionCall;
        this.cooldown = cooldown;
        this.condition = condition;
        this.coroutineClock = coroutineClock;
    }
    
    
    public void Execute()
    {
        if(!canAct || !condition.IsMet()) return;
        
        canAct = false;
        
        actionCall.Call();
        coroutineClock.Schedule(() =>
            {
                canAct = true;
            }
            , cooldown);
    }

    public bool IsMet()
        => canAct;
}