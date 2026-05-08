using System;

public sealed class DelecateCondition
    : Condition
{
    private readonly Func<bool> condition;


    public DelecateCondition(Func<bool> condition)
    {
        this.condition = condition;
    }


    public bool IsMet()
        => condition();
}