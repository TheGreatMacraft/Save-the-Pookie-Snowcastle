using System.Collections.Generic;

public sealed class FilteredTarget : Filter<Target>
{
    private readonly Target target;
    private readonly string requiredTag;

    private List<Target> value = new(1);
    private List<bool> passed = new(1);
    
    public FilteredTarget(
        Target target,
        string requiredTag
        )
    {
        this.target = target;
        this.requiredTag = requiredTag;
    }


    public Target Value()
    {
        if (value.Count == 0)
            value.Add(
                Passed()
                ? target
                : new NullTarget()
                );
        
        return value[0];
    }

    public bool Passed()
    {
        if(passed.Count == 0)
            passed.Add(
                target.IsTaggedAs(requiredTag)
                );

        return passed[0];
    }
}