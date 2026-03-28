using System;

public sealed class InstantAction : ActionExecution
{
    private readonly ActionCall actionCall;
    private readonly float cooldown;
    private readonly Clock coroutineClock;

    private bool canAct = true;


    public InstantAction(ActionCall actionCall, float cooldown,  Clock coroutineClock)
    {
        this.actionCall = actionCall;
        this.cooldown = cooldown;
        this.coroutineClock = coroutineClock;
    }
    
    
    public void Execute()
    {
        if(!canAct) return;
        
        canAct = false;
        
        actionCall.Call();
        coroutineClock.Schedule(() =>
            {
                canAct = true;
            }
            , cooldown);
    }
}