public sealed class NullPlayerState : PlayerState
{
    private readonly State nullState = new NullState();
    
    public State CurrentState() => nullState;
    public void SetState(State state) {}
    public void Toggle() {}
}