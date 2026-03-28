using System;

public sealed class DelayedAction : ActionExecution
{
    private readonly ActionCall actionCall;
    private readonly float cooldown;
    private readonly Clock coroutineClock;

    private bool canAct = true;


    public DelayedAction(ActionCall actionCall, float cooldown, Clock coroutineClock)
    {
        this.actionCall = actionCall;
        this.cooldown = cooldown;
        this.coroutineClock = coroutineClock;
    }
    
    
    public void Execute()
    {
        if(!canAct) return;
        
        canAct = false;
        
        coroutineClock.Schedule(() =>
            {
                actionCall.Call();
                canAct = true;
            }
            , cooldown);
    }
}