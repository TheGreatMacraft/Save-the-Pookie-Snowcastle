public sealed class OnPressed : 
    Condition
{
    private readonly InputActionState inputActionState;
    
    
    public OnPressed(InputActionState inputActionState)
    {
        this.inputActionState = inputActionState;
    }


    public bool IsMet()
        => inputActionState.WasPressedThisFrame();
}


public sealed class OnReleased : 
    Condition
{
    private readonly InputActionState inputActionState;
    
    
    public OnReleased(InputActionState inputActionState)
    {
        this.inputActionState = inputActionState;
    }


    public bool IsMet()
        => inputActionState.WasReleasedThisFrame();
}

public sealed class OnBeingPressed : 
    Condition
{
    private readonly InputActionState inputActionState;

    
    public OnBeingPressed(InputActionState inputActionState)
    {
        this.inputActionState = inputActionState;
    }


    public bool IsMet()
        => inputActionState.IsPressed();
}

public sealed class OnBeingReleased :
    Condition
{
    private readonly InputActionState inputActionState;


    public OnBeingReleased(InputActionState inputActionState)
    {
        this.inputActionState = inputActionState;
    }


    public bool IsMet()
        => inputActionState.IsReleased();
}