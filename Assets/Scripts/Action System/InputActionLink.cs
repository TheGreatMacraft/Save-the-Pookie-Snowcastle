public sealed class InputActionLink
    : ActionInterpreter
{
    private readonly ActionExecution actionExecution;
    private readonly InputTrigger inputTrigger;
    private readonly bool extraCondition;
    
    
    public InputActionLink(
        ActionExecution actionExecution,
        InputTrigger inputTrigger,
        bool extraCondition = true
        )
    {
        this.actionExecution = actionExecution;
        this.inputTrigger = inputTrigger;
        this.extraCondition = extraCondition;
    }
    
    
    public void ExecuteActionCall()
    {
        if(inputTrigger.IsActive() && extraCondition)
            actionExecution.Execute();
    }
}