using UnityEngine;

public sealed class WeaponPayload : 
    Payload
{
    private readonly string targetTag;
    private readonly Impact impact;
    private readonly Terminable disposableHitter;


    public WeaponPayload(
        string targetTag,
        Impact impact,
        Terminable disposableHitter
    )
    {
        this.targetTag = targetTag;
        this.impact = impact;
        this.disposableHitter = disposableHitter;
    }


    public void Deliver(GameObject potentialTarget)
    {
        new FilteredTarget(
                new ComponentInObject<Target>(
                    potentialTarget, 
                    new NullTarget()
                ).Value(),
                targetTag
            ).Value()
            .Hit(impact, disposableHitter);
    }

    public void Deliver(ReadOnlyCollection<GameObject> potentialTargets)
    {
        foreach (
            GameObject potentialTarget 
            in potentialTargets.AllElements())
        {
            new FilteredTarget(
                    new ComponentInObject<Target>(
                        potentialTarget, 
                        new NullTarget()
                    ).Value(),
                    targetTag
                ).Value()
                .Hit(impact, disposableHitter);
        }
    }
}