using System.Collections.Generic;

public sealed class IsSameCondition<T> : Condition
{
    private readonly Scalar<T> registry;
    private readonly T origin;


    public IsSameCondition(Scalar<T> registry, T origin)
    {
        this.registry = registry;
        this.origin = origin;
    }


    public bool IsMet()
        => EqualityComparer<T>.Default.Equals(registry.Value(), origin);
}