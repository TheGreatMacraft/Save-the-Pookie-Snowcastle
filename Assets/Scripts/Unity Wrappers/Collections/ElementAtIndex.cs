public sealed class ElementAtIndex<T> : Scalar<T>
{
    private readonly ReadOnlyCollection<T> collection;
    private readonly Scalar<int> index;


    public ElementAtIndex(
        ReadOnlyCollection<T> collection,
        Scalar<int> index
    )
    {
        this.collection = collection;
        this.index = index;
    }
    
    
    public T Value() => collection.ElementAt(index.Value());
}