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
    
    private PlayerState playerState;
    
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
    
    
    private PlayerState PlayerState()
        => playerState ??=
            new ComponentInObject<PlayerState>(
                new ParentOfGameObject(gameObject).Value(),
                new NullPlayerState()
            ).Value();
    

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
                new AndConditions(
                    PlayerMovement().RollAction().Concluded(),
                    new IsStateCondition(PlayerState(), new BattleState())
                )
            );

    
    private void Update()
    {
        WeaponOrientation().Orient();
        ModelPresentation().Present();
    }
}