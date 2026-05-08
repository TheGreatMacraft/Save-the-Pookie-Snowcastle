public sealed class IsIdentityCondition<T> : Condition
    where T : Identity
{
    private readonly Context<T> context;
    private readonly Identity compareIdentity;

    
    public IsIdentityCondition(Context<T> context, Identity compareIdentity)
    {
        this.context = context;
        this.compareIdentity = compareIdentity;
    }


    public bool IsMet()
        => context.Current().Matches(compareIdentity);
}