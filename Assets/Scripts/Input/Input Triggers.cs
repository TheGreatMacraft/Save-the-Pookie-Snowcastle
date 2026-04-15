public sealed class OnPressed : 
    InputTrigger
{
    private readonly InputActionState inputActionState;
    
    
    public OnPressed(InputActionState inputActionState)
    {
        this.inputActionState = inputActionState;
    }


    public bool IsActive()
        => inputActionState.WasPressedThisFrame();
}


public sealed class OnReleased : 
    InputTrigger
{
    private readonly InputActionState inputActionState;
    
    
    public OnReleased(InputActionState inputActionState)
    {
        this.inputActionState = inputActionState;
    }


    public bool IsActive()
        => inputActionState.WasReleasedThisFrame();
}

public sealed class OnBeingPressed : 
    InputTrigger
{
    private readonly InputActionState inputActionState;

    
    public OnBeingPressed(InputActionState inputActionState)
    {
        this.inputActionState = inputActionState;
    }


    public bool IsActive()
        => inputActionState.IsPressed();
}

public sealed class OnBeingReleased : 
    InputTrigger
{
    private readonly InputActionState inputActionState;

    
    public OnBeingReleased(InputActionState inputActionState)
    {
        this.inputActionState = inputActionState;
    }


    public bool IsActive()
        => inputActionState.IsReleased();
}

public sealed class InputTriggerCombo :
    InputTrigger
{
    private readonly ReadOnlyCollection<InputTrigger>  inputTriggers;


    public InputTriggerCombo(
        ReadOnlyCollection<InputTrigger> inputTriggers
    )
    {
        this.inputTriggers = inputTriggers;
    }


    public bool IsActive()
    {
        foreach (
            InputTrigger trigger 
            in inputTriggers.AllElements()
            )
        {
            if(!trigger.IsActive())
                return false;
        }
        
        return true;
    }
}