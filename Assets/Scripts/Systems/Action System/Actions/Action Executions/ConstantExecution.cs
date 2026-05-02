using UnityEngine;

public sealed class ConstantExecution
    : ActionExecution
{
    private readonly ActionCall call;
    private Condition trueCondition = new TrueCondition();
    private Condition falseCondition = new FalseCondition();
    
    
    public ConstantExecution(ActionCall call)
    {
        this.call = call;
    }


    public void Execute()
    {
        call.Call();
    }
    
    public Condition Concluded() => trueCondition;
    public Condition InProgress() => falseCondition;
}