public sealed class InputActionLink
    : ActionInterpreter
{
    private readonly ActionExecution actionExecution;
    private readonly InputTrigger inputTrigger;


    public InputActionLink(
        ActionExecution actionExecution,
        InputTrigger inputTrigger
        )
    {
        this.actionExecution = actionExecution;
        this.inputTrigger = inputTrigger;
    }
    
    
    public void ExecuteActionCall()
    {
        if(inputTrigger.IsActive())
            actionExecution.Execute();
    }
}