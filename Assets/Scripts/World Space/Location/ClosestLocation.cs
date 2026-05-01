public sealed class ClosestLocation<T> : 
    Scalar<T> where T : Location
{
    private readonly Location origin;
    private ReadOnlyCollection<T> targets;

    private readonly T nullValue;
    

    public ClosestLocation(
        Location origin,
        ReadOnlyCollection<T> targets,
        T nullValue
        )
    {
        this.origin = origin;
        this.targets = targets;
        this.nullValue = nullValue;
    }
    

    public T Value()
    {
        T selectedValue = nullValue;
        float minDistance = float.MaxValue;
        
        foreach (T potentialTarget in targets.AllElements())
        {
            float currentDistance = new DistanceBetweenLocations(
                origin,
                potentialTarget
            ).Value();

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                selectedValue = potentialTarget;
            }
        }
        
        return selectedValue;
    }
}