public sealed class EngineerClass
    : BaseClass
{
    public EngineerClass(
        Visibility hologram,
        PhysicalBody hologramBody,
        BuildingComponent building,
        PlayerState playerState,
        Clock coroutineClock,
        InputActionStates inputActionStates
    )
        : base(
            new SimpleReadOnlyCollection<ActionExecution>(
                
                // Toggle Hologram
                new OnChangeExecution(
                    new ConstantExecution(new ToggleCall(hologram)),
                    new IsStateCondition(playerState, new BuildState())
                ),
                
                // Place Hologram
                new ConditionalExecution(
                    new ExecutionWithCooldown(
                        new ConstantExecution(
                            new SpawnBuildingCall(
                                new ConstantGameObjectBuilder<BuildingComponent>(
                                    building,
                                    hologramBody
                                )
                            )
                        ),
                        1f,
                        coroutineClock,
                        false
                    ),
                    new OnPressed(
                        inputActionStates.PrimaryActionState()
                    )
                )
            ),
            1f,
            1.5f
        ) {}
}