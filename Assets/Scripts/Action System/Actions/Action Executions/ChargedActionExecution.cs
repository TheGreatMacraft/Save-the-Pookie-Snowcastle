using UnityEngine;

public sealed class ChargedActionExecution
    : ActionExecution
{
    private readonly ActionCall chargeCall;
    private readonly ActionCall cancelCall;
    private readonly ActionExecution finalAction;
    private readonly float chargeTime;
    private readonly Clock coroutineClock;
    private readonly Condition chargeupConditions;

    private bool isCharging = false;

    
    public ChargedActionExecution(
        ActionCall chargeCall, 
        ActionExecution finalAction, 
        float chargeTime, 
        Clock coroutineClock,
        Condition chargeupConditions
        )
        : this(
            chargeCall,
            finalAction,
            chargeTime, 
            coroutineClock,
            chargeupConditions,
            new NullActionCall()
            ) {}
    
    public ChargedActionExecution(
        ActionCall chargeCall, 
        ActionExecution finalAction, 
        float chargeTime, 
        Clock coroutineClock,
        Condition chargeupConditions,
        ActionCall cancelCall
        )
    {
        this.chargeCall = chargeCall;
        this.finalAction = finalAction;
        this.chargeTime = chargeTime;
        this.coroutineClock = coroutineClock;
        this.chargeupConditions = chargeupConditions;
        this.cancelCall = cancelCall;
    }

    public void Execute()
    {
        if (isCharging
            || !finalAction.Concluded().IsMet()
            || !chargeupConditions.IsMet()
            ) return;
        
        isCharging = true;
        chargeCall.Call();
        
        coroutineClock.Schedule(() =>
            {
                Debug.Log("Heavy Attack");
                isCharging = false;
                finalAction.Execute();
            }, 
            chargeTime,
            chargeupConditions,
            () =>
            {
                isCharging = false;
                cancelCall.Call();
            }
        );
    }
    
    public bool IsMet() 
        => !isCharging && finalAction.Concluded().IsMet();
}