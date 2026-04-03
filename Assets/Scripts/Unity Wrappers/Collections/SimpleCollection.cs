using System.Collections.Generic;

public sealed class SimpleCollection<T> : Collection<T>
{
    List<T> elements;

    public SimpleCollection()
        : this(new List<T>()) {}
    
    public SimpleCollection(
        ReadOnlyCollection<T> collection)
        : this(collection.Copy()) {}

    private SimpleCollection(List<T> list)
    {
        elements = list;
    }

    
    public void Register(T newElement)
    {
        elements.Add(newElement);
    }

    public void Unregister(T newElement)
    {
        if (elements.Contains(newElement))
            elements.Remove(newElement);
    }
    
    public IEnumerable<T> AllElements()
        => elements;
}