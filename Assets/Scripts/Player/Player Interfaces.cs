public interface PlayerMovement
{
    public ActionExecution Movement();
    public ActionExecution RollAction();
}


// Player State

public interface PlayerState : Context<State>, Togglable {}
public interface State : Identity {}