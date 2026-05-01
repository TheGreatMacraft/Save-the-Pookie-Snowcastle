using System.Collections.Generic;
using UnityEngine;

public sealed class UnityGameObject
{
    private readonly GameObject gameObject;
    
    private readonly List<GameObject> parent;
    private readonly List<ReadOnlyCollection<GameObject>> children;
    
    
    public UnityGameObject(MonoBehaviour component)
        : this(component.gameObject) {}
    
    public UnityGameObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    
    
    public GameObject Parent()
    {
        if (parent.Count == 0)
            parent.Add(new ParentOfGameObject(gameObject).Parent());

        return parent[0];
    }

    public ReadOnlyCollection<GameObject> Children()
    {
        if(children.Count == 0)
            children.Add(new ChildrenOfGameObject(gameObject).Children());
        
        return children[0];
    }
}