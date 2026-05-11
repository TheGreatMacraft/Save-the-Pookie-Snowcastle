public interface InputAction
{
    string ToString();
}

public interface Visibility : Togglable
{
    void Hide();
    void Show();
    Condition IsVisible();
    
    void Togglable.Toggle()
    {
        if(IsVisible().IsMet())
            Hide();
        else
            Show();
    }
}

public interface Togglable
{
    public void Toggle();
}