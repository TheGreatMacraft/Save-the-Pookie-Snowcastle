public sealed class NullScalar<T> : Scalar<T>
{
    private readonly T nullValue;
    
    
    public NullScalar(T nullValue)
    {
        this.nullValue = nullValue;
    }
    
    
    public T Value() => nullValue;
}