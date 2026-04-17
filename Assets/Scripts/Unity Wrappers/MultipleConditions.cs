public sealed class MultipleConditions
    : Condition
{
    private readonly ReadOnlyCollection<Condition> conditions;
    
    
    public MultipleConditions(params Condition[] elements)
        : this(new SimpleReadOnlyCollection<Condition>(elements)) {}

    private MultipleConditions(ReadOnlyCollection<Condition> conditions)
    {
        this.conditions = conditions;
    }

    public bool IsMet()
    {
        foreach (Condition condition in conditions.AllElements())
            if (!condition.IsMet())
                return false;

        return true;
    }
}