using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class BuildingComponent
    : MonoBehaviour, Building
{
    [SerializeField] private string targetTag;

    private Location buildingLocation;
    private TargetScouter targetScouter;


    private Location BuildingLocation()
        => buildingLocation ??=
            new ComponentInObject<PhysicalBody>(
                gameObject,
                new NullPhysicalBody()
            ).Value();

    private TargetScouter TargetScouter()
        => targetScouter ??=
            new TargetScouter(
                new ClosestTarget(
                    BuildingLocation(),
                    targetTag
                )
            );

    
    private void Awake()
    {
        TargetScouter().FindNewTarget();
    }
    
    // Proxy
    public Vector3 Coordinates()
        => targetScouter.CurrentTarget().Coordinates();
}