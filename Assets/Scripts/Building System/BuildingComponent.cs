using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class BuildingComponent
    : MonoBehaviour, TargetLocationSource
{
    [SerializeField] private string targetTag;
    
    private TargetScouter targetScouter;
    private Target currentTarget;

    
    private void Awake()
    {
        Location buildingLocation = new ComponentInObject<PhysicalBody>(
            gameObject,
            new NullPhysicalBody()
        ).Value();

        targetScouter = new TargetScouter(
            new ClosestTarget(
                buildingLocation,
                targetTag
            )
        );
        
        targetScouter.FindNewTarget();
    }
    
    // Proxy
    public Vector3 Coordinates()
        => targetScouter.CurrentTarget().Coordinates();
}