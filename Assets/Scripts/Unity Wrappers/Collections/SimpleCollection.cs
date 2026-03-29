using System.Collections.Generic;

public sealed class SimpleCollection<T> : Collection<T>
{
    List<T> elements = new();

    
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