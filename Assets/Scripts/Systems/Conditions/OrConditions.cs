public sealed class OrConditions
    : Condition
{
    private readonly ReadOnlyCollection<Condition> conditions;
    
    
    public OrConditions(params Condition[] elements)
        : this(new SimpleReadOnlyCollection<Condition>(elements)) {}

    private OrConditions(ReadOnlyCollection<Condition> conditions)
    {
        this.conditions = conditions;
    }

    public bool IsMet()
    {
        foreach (Condition condition in conditions.AllElements())
            if (condition.IsMet())
                return true;

        return false;
    }
}