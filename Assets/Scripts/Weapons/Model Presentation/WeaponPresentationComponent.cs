using UnityEngine;

[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class WeaponPresentationComponent :
    MonoBehaviour
{
    [Header("Sprite Renderer")]
    [SerializeField] private SpriteRenderer spriteRenderer;


    private GameObject parent;
    private PlayerMovement playerMovement;
    private PhysicalBody weaponAnchor;
    private TargetLocationSource targetLocation;
    private Vector orientationVector;
    
    private Presentation modelPresentation;
    private Orientation weaponOrientation;


    private GameObject Parent()
        => parent ??=
            new ParentOfGameObject(gameObject).Value();

    private PlayerMovement PlayerMovement()
        => playerMovement ??=
            new ComponentInObject<PlayerMovement>(
                Parent(),
                new NullPlayerMovement()
            ).Value();

    private PhysicalBody WeaponAnchor()
        => weaponAnchor ??=
            new ComponentInObject<PhysicalBody>(
                gameObject,
                new NullPhysicalBody()
            ).Value();

    private TargetLocationSource TargetLocation()
        => targetLocation ??=
            new ComponentInObject<TargetLocationSource>(
                Parent(),
                new NullTargetLocationSource()
            ).Value();
    
    private Vector OrientationVector()
        => orientationVector ??=
            new Vector(
                new PointToPointVectorDefinition(
                    WeaponAnchor(),
                    TargetLocation()
                )
            );

    private Orientation WeaponOrientation()
        => weaponOrientation ??=
            new WeaponOrientation(
                WeaponAnchor(),
                OrientationVector(),
                spriteRenderer
            );
    
    private Presentation ModelPresentation()
        => modelPresentation ??=
            new ConditionalVisibility(
                new SpriteVisibility(spriteRenderer),
                PlayerMovement().RollAction().Concluded()
            );

    
    private void Update()
    {
        WeaponOrientation().Orient();
        ModelPresentation().Present();
    }
}