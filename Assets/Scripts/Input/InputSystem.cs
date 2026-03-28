using UnityEngine.InputSystem;

public class InputSystem : IInputAction
{
    private PlayerInput playerInput;

    private InputActionMap inputActionMap = new InputActionMap();
    
    public InputSystem(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    public bool PressedThisFrame(InputAction inputAction)
    {
        return playerInput.actions[inputActionMap.GetStringOf(inputAction)].WasPerformedThisFrame();
    }
}