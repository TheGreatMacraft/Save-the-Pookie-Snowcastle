public class InputInterpreter : ActionInterpreter
{
    private ActionExecution primaryAction;
    private ActionExecution secondaryAction;
    private ActionExecution abilityAction;
    private InputSystem inputSystem;
    
    
    public InputInterpreter(
        ActionExecution primaryAction,
        ActionExecution secondaryAction,
        ActionExecution abilityAction,
        InputSystem inputSystem
    )
    {
        this.primaryAction = primaryAction;
        this.secondaryAction = secondaryAction;
        this.abilityAction = abilityAction;
        this.inputSystem = inputSystem;
    }

    
    public void ExecuteActionCalls()
    {
        if(inputSystem.PressedThisFrame(InputAction.PRIMARY))
            primaryAction.Execute();
        
        if (inputSystem.PressedThisFrame(InputAction.SECONDARY))
            secondaryAction.Execute();
        
        if(inputSystem.PressedThisFrame(InputAction.ABILITY))
            abilityAction.Execute();
    }
}