using System.Collections.Generic;
using UnityEngine;

public sealed class ComponentsInObjects<T> : 
    ReadOnlyCollection<T>
{
    private readonly IEnumerable<GameObject> objects;
    private readonly T nullObject;
    
    
    public  ComponentsInObjects(
        IEnumerable<GameObject> objects,
        T nullObject
        )
    {
        this.objects = objects;
        this.nullObject = nullObject;
    }


    public IEnumerable<T> AllElements()
    {
        foreach (GameObject obj in objects)
        {
            yield return new ComponentInObject<T>(
                obj,
                nullObject
                ).Value();
        }
    }
}