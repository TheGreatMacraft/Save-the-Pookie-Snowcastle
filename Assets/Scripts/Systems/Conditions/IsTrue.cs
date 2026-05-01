using System;

public sealed class IsTrue
    : Condition
{
    private readonly Func<bool> predicate;


    public IsTrue(Func<bool> predicate)
    {
        this.predicate = predicate;
    }


    public bool IsMet() => predicate();
}