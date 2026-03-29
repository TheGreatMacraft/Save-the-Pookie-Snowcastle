using System.Collections.Generic;
using UnityEngine;

public sealed class AllComponentsInObject<T>
{
    private readonly GameObject origin;
    private readonly T nullObject;
    private ReadOnlyCollection<T> cache = new NullCollection<T>();
    
    
    public AllComponentsInObject(Collider2D collider, T nullObject)
        : this(collider.gameObject, nullObject) {}

    public AllComponentsInObject(GameObject origin, T nullObject)
    {
        this.origin = origin;
        this.nullObject = nullObject;
    }
    
    
    public ReadOnlyCollection<T> Value()
    {
        if (cache.Count() == 0)
        {
            cache = new SimpleReadOnlyCollection<T>(
                origin.GetComponents<T>()
            );

            if (cache.Count() == 0)
                cache = new SimpleReadOnlyCollection<T>(
                    new List<T> {nullObject});
        }

        return cache;
    }
}