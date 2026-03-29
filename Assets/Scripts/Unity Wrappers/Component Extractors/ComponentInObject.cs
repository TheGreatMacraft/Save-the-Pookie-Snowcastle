using System.Collections.Generic;
using UnityEngine;

public sealed class ComponentInObject<T>
{
    private readonly GameObject origin;
    private readonly T nullObject;
    private List<T> cache = new(1);
    
    
    public ComponentInObject(Collider2D collider, T nullObject)
        : this(collider.gameObject, nullObject) {}

    public ComponentInObject(GameObject origin, T nullObject)
    {
        this.origin = origin;
        this.nullObject = nullObject;
    }
    

    public T Value()
    {
        if (cache.Count == 0)
        {
            if (origin.TryGetComponent<T>(out var found))
                cache.Add(found);
            else
                cache.Add(nullObject);
        }
        
        return cache[0];
    }
}