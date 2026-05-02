public sealed class Not
    : Condition
{
    private readonly Condition condition;


    public Not(Condition condition)
    {
        this.condition = condition;
    }
    
    
    public bool IsMet()
        => !condition.IsMet();
}