using System.Collections.Generic;

public sealed class ChargedActionExecution
    : ActionExecution
{
    private readonly ActionCall onStartCall;
    private readonly ActionCall onCancelCall;
    private readonly ActionExecution finalAction;
    private readonly float chargeTime;
    private readonly Clock coroutineClock;
    private readonly Condition chargeupConditions;

    private bool isCharging = false;
    private List<Condition> inProgressCondition = new(1);
    private List<Condition> concludedCondition = new(1);
    
    
    public ChargedActionExecution(
        ActionCall onStartCall, 
        ActionCall onCancelCall,
        ActionExecution finalAction, 
        float chargeTime, 
        Clock coroutineClock,
        Condition chargeupConditions
        )
    {
        this.onStartCall = onStartCall;
        this.onCancelCall = onCancelCall;
        this.finalAction = finalAction;
        this.chargeTime = chargeTime;
        this.coroutineClock = coroutineClock;
        this.chargeupConditions = chargeupConditions;
    }

    public void Execute()
    {
        if (isCharging
            || !finalAction.Concluded().IsMet()
            || !chargeupConditions.IsMet()
            ) return;
        
        isCharging = true;
        onStartCall.Call();
        
        coroutineClock.Schedule(() =>
            {
                isCharging = false;
                finalAction.Execute();
            }, 
            chargeTime,
            chargeupConditions,
            () =>
            {
                isCharging = false;
                onCancelCall.Call();
            }
        );
    }

    public Condition InProgress()
    {
        if (inProgressCondition.Count == 0)
        {
            inProgressCondition.Add(new Not(Concluded()));
        }
        
        return inProgressCondition[0];
    }
    
    public Condition Concluded()
    {
        if (concludedCondition.Count == 0)
        {
            concludedCondition.Add(
                new AndConditions(
                    new IsTrue(() => !isCharging),
                    finalAction.Concluded()
                )
            );
        }
        
        return concludedCondition[0];
    }
}