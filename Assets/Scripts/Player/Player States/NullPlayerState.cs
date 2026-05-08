public sealed class NullPlayerState : PlayerState
{
    private readonly State nullState = new NullState();
    
    public State Current() => nullState;
    public void TransitionTo(State state) {}
    public void Toggle() {}
}