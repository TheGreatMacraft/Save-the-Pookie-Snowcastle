using System.Collections.Generic;
using System.Linq;

public sealed class SimpleReadOnlyCollection<T> : 
    ReadOnlyCollection<T>
{
    private readonly IEnumerable<T> collection;
    
    
    public  SimpleReadOnlyCollection(IEnumerable<T> collection)
    {
        this.collection = collection.ToList();
    }


    public IEnumerable<T> AllElements()
        => collection;
}