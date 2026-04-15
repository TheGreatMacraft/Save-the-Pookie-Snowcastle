using UnityEngine.InputSystem;

public sealed class InputActionState
{
    private readonly PlayerInput playerInput;
    private readonly InputAction action;
    
    
    public InputActionState(
        PlayerInput playerInput,
        InputAction action
        )
    {
        this.playerInput = playerInput;
        this.action = action;
    }


    public bool WasPressedThisFrame()
        => playerInput.actions[action.ToString()].WasPressedThisFrame();
    
    public bool IsPressed()
        => playerInput.actions[action.ToString()].IsPressed();
    
    public bool WasReleasedThisFrame()
        => playerInput.actions[action.ToString()].WasReleasedThisFrame();

    public bool IsReleased()
        => !IsPressed();

    // Room for more, when necesarry

}