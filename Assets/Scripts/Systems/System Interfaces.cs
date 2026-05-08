using System.Collections.Generic;


// Value Holder
public interface Scalar<T>
{
    T Value();
}

// Index
public interface Index : Scalar<int>
{
    void SetTo(int value);
    void Increment();
    void Decrement();
}


// Identity & Context
public interface Identity
{
    string Name();
    public bool Matches(Identity other)
        => Name().Equals(other.Name());
}

public interface Context<T> where T : Identity
{
    void TransitionTo(T identity);
    T Current();
}


// Collection & Read-only Collection
public interface ReadOnlyCollection<T>
{
    IEnumerable<T> AllElements();

    List<T> Copy()
        => new List<T>(AllElements());

    T ElementAt(int index)
        => Copy()[index];
    
    public int Count()
    {
        int count = 0;
        using var enumerator = AllElements().GetEnumerator();
        while (enumerator.MoveNext()) count++;
        return count;
    }
}

public interface Condition
{
    public bool IsMet();
}


// Disablable
public interface Disablable
{
    public bool IsEnabled();
    public void Disable();
    public void Enable();
}


// Collection
public interface Collection<T> :  ReadOnlyCollection<T>
{
    void Register(T element);
    void Unregister(T element);
}