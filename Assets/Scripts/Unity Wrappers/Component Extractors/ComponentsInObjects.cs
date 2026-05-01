using System.Collections.Generic;
using UnityEngine;

public sealed class ComponentsInObjects<T> : 
    ReadOnlyCollection<T>
{
    private readonly ReadOnlyCollection<GameObject> objects;
    private readonly T nullObject;
    
    
    public  ComponentsInObjects(
        ReadOnlyCollection<GameObject> objects,
        T nullObject
        )
    {
        this.objects = objects;
        this.nullObject = nullObject;
    }


    

    public IEnumerable<T> AllElements()
    {
        foreach (GameObject obj in objects.AllElements())
        {
            yield return new ComponentInObject<T>(
                obj,
                nullObject
                ).Value();
        }
    }
}