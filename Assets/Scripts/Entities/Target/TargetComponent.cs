using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class TargetComponent : 
    MonoBehaviour, 
    Target
{
    private Tagged taggedObject;
    private Location targetLocation;

    
    private Tagged TaggedObject()
        => taggedObject ??=
            new TaggedObject(
                gameObject.tag
            );
    
    private Location TargetLocation()
        => targetLocation ??=
            new ComponentInObject<Location>(
                gameObject,
                new NullLocation()
            ).Value();


    public void Hit(Impact impact, Terminable disposableHitter)
    {
        impact.ApplyOn(gameObject);
        disposableHitter.Terminate();
    }
    
    public Vector3 Coordinates()
        => TargetLocation().Coordinates();
    
    public bool IsTaggedAs(string checkTag)
        => TaggedObject().IsTaggedAs(checkTag);
}