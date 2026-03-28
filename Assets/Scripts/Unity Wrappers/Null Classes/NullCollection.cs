using System.Collections.Generic;
using System.Linq;

public class NullCollection<T> : Collection<T>
{
    public void Register(T element) {}
    public void Unregister(T element) {}

    public IEnumerable<T> AllElements()
        => Enumerable.Empty<T>();
}