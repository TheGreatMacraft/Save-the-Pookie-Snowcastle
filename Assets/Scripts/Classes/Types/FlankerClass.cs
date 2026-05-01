public sealed class FlankerClass
    : BaseClass
{
    public FlankerClass()
        : base(
            new SimpleReadOnlyCollection<ActionExecution>(),
            1.5f,
            1.5f
        ) {}
}