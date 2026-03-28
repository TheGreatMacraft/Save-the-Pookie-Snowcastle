using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class TargetComponent : 
    MonoBehaviour, 
    Target
{
    private Tagged taggedObject;
    private Location targetLocation;


    private void Awake()
    {
        taggedObject = new TaggedObject(
            gameObject.tag
            );

        targetLocation = new ComponentInObject<Location>(
            gameObject,
            new NullLocation()
        ).Value();
    }


    public void Hit(Impact impact, Terminable disposableHitter)
    {
        impact.ApplyOn(gameObject);
        disposableHitter.Terminate();
    }
    
    public Vector3 Coordinates()
        => targetLocation.Coordinates();
    
    public bool IsTaggedAs(string checkTag)
        => taggedObject.IsTaggedAs(checkTag);
}