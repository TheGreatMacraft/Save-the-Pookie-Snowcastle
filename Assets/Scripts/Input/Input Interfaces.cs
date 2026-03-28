public interface IInputAction
{
    bool PressedThisFrame(InputAction inputAction);
}

public interface IInputActionMap
{
    string GetStringOf(InputAction inputAction);
}

public interface Visibility
{
    void Hide();
    void Show();
}