using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectsWithTag: 
    ReadOnlyCollection<GameObject>
{
    private readonly string groupTag;
    
    
    public GameObjectsWithTag(
        string groupTag
        )
    {
        this.groupTag = groupTag;
    }


    // Apart from using a singleton, this is the only way (is unoptimized)
    public IEnumerable<GameObject> AllElements()
        => new List<GameObject>(
            GameObject.FindGameObjectsWithTag(groupTag)
            ).AsEnumerable();
}