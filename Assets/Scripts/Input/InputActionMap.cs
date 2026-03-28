using System.Collections.Generic;

public enum InputAction
{
    PRIMARY,
    SECONDARY,
    ABILITY,
    TOOL
}

public class InputActionMap : IInputActionMap
{
    private Dictionary<InputAction, string> inputActionMap = new Dictionary<InputAction, string>()
    {
        { InputAction.PRIMARY, "Primary" },
        { InputAction.SECONDARY, "Secondary" },
        { InputAction.ABILITY, "Ability" },
        { InputAction.TOOL, "Tool" }
    };

    public string GetStringOf(InputAction inputAction)
    {
        return inputActionMap[inputAction];
    }
}