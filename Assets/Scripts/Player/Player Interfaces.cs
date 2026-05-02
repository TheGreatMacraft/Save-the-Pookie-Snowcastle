public interface PlayerMovement
{
    public ActionExecution Movement();
    public ActionExecution RollAction();
}

public interface PlayerState : Togglable
{
    public void SetState(State state);
    public State CurrentState();
}

public interface State
{
    string Name();
    public bool Equals(State other) 
        => Name().Equals(other.Name());
}