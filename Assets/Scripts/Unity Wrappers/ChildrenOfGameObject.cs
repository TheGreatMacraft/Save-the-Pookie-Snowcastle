using System.Collections.Generic;
using UnityEngine;

public sealed class ChildrenOfGameObject
{
    private readonly Transform gameObject;

    private readonly List<Collection<GameObject>> childObjects = new(1);
    
    
    public ChildrenOfGameObject(GameObject gameObject)
        : this(gameObject.transform) {}
    
    private ChildrenOfGameObject(Transform gameObject)
    {
        this.gameObject = gameObject;
    }
    

    public ReadOnlyCollection<GameObject> Children()
    {
        if (childObjects.Count == 0)
        {
            childObjects.Add(new SimpleCollection<GameObject>());
            foreach (Transform child in gameObject.transform)
            {
                childObjects[0].Register(child.gameObject);
            }
        }
        
        return childObjects[0];
    }
}