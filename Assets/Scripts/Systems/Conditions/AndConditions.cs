public sealed class AndConditions
    : Condition
{
    private readonly ReadOnlyCollection<Condition> conditions;
    
    
    public AndConditions(params Condition[] elements)
        : this(new SimpleReadOnlyCollection<Condition>(elements)) {}

    private AndConditions(ReadOnlyCollection<Condition> conditions)
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