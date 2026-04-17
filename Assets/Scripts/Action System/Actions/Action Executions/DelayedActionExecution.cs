public sealed class DelayedAction 
    : ActionExecution
{
    private readonly ActionCall actionCall;
    private readonly float cooldown;
    private readonly Condition condition;
    private readonly Clock coroutineClock;

    private bool canAct = true;


    public DelayedAction(
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
        
        coroutineClock.Schedule(() =>
            {
                actionCall.Call();
                canAct = true;
            }
            , cooldown);
    }
    
    public bool IsMet()
        => canAct;
}