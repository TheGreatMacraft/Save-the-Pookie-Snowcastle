public sealed class EngineerClass
    : BaseClass
{
    public EngineerClass(
        Togglable hologram,
        PlayerState playerState
    )
        : base(
            new SimpleReadOnlyCollection<ActionExecution>(
                new OnChangeExecution(
                    new ToggleCall(hologram),
                    new IsStateCondition(playerState, new BuildState())
                )
            ),
            1f,
            1.5f
        ) {}
}