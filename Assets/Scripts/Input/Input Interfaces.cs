public interface InputAction
{
    string ToString();
}

public interface InputTrigger
{
    bool IsActive();
}

public interface Visibility
{
    void Hide();
    void Show();
}