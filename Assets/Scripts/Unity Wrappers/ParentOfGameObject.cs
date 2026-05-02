using System.Collections.Generic;
using UnityEngine;

public sealed class ParentOfGameObject
    : Scalar<GameObject>
{
    private readonly Transform gameObject;

    private readonly List<GameObject> parentObject = new(1);
    
    public ParentOfGameObject(GameObject gameObject)
        : this(gameObject.transform) {}
    
    private ParentOfGameObject(Transform gameObject)
    {
        this.gameObject = gameObject;
    }

    public GameObject Value()
    {
        if (parentObject.Count == 0)
        {
            parentObject.Add(gameObject.parent.gameObject);
        }
        
        return parentObject[0];
    }
}