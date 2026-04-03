using System.Collections.Generic;

public sealed class SimpleReadOnlyCollection<T> : 
    ReadOnlyCollection<T>
{
    private readonly IEnumerable<T> collection;

    public SimpleReadOnlyCollection(params T[] elements)
    {
        this.collection = elements;
    }
    
    public SimpleReadOnlyCollection(List<T> collection)
    {
        this.collection = collection;
    }
    
    public  SimpleReadOnlyCollection(IEnumerable<T> collection)
    {
        this.collection = collection;
    }


    public IEnumerable<T> AllElements()
        => collection;
}