public sealed class SpawnBuildingCall
    : ActionCall
{
    private readonly ConstantGameObjectBuilder<BuildingComponent> builder;
    
    
    public SpawnBuildingCall(
        ConstantGameObjectBuilder<BuildingComponent> builder
    )
    {
        this.builder = builder;
    }


    public void Call()
    {
        builder.Build();
    }
}