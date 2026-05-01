public class IsStateCondition : Condition
{
    private readonly PlayerState playerState;
    private readonly State compareState;

    
    public IsStateCondition(PlayerState playerState, State compareState)
    {
        this.playerState = playerState;
        this.compareState = compareState;
    }


    public bool IsMet()
        => playerState.CurrentState().Equals(compareState);
}