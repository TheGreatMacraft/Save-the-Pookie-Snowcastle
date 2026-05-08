public interface InputAction
{
    string ToString();
}

public interface Visibility : Togglable
{
    public void Hide();
    public void Show();
    public Condition IsVisible();
    
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