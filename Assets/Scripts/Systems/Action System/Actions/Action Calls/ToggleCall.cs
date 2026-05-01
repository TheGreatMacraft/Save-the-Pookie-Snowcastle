public sealed class ToggleCall
    : ActionCall
{
    private readonly Togglable togglable;


    public ToggleCall(Togglable togglable)
    {
        this.togglable = togglable;
    }


    public void Call()
    {
        togglable.Toggle();
    }
}