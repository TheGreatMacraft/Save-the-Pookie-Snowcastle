using System.Collections.Generic;
using UnityEngine;

public sealed class AllComponentsInObject<T>
{
    private readonly GameObject origin;
    private readonly T nullObject;
    private List<T> cache = new();
    
    
    public AllComponentsInObject(Collider2D collider, T nullObject)
        : this(collider.gameObject, nullObject) {}

    public AllComponentsInObject(GameObject origin, T nullObject)
    {
        this.origin = origin;
        this.nullObject = nullObject;
    }
    

    public List<T> Value()
    {
        if (cache.Count == 0)
        {
            cache.AddRange(origin.GetComponents<T>());
            
            if(cache.Count == 0)
                cache.Add(nullObject);
        }

        return cache;
    }
}