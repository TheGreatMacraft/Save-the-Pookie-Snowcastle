using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class WeaponPresentationComponent :
    MonoBehaviour
{
    [Header("Sprite Renderer")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private PlayerMovement playerMovement;
    private Presentation modelPresentation;
    
    private Orientation weaponOrientation;
    
    
    private void Awake()
    {
        // Player Movement - Works even for Entity (NullPlayerMovement)
        playerMovement = new ComponentInObject<PlayerMovement>(
            new ParentOfGameObject(gameObject).Parent(),
            new NullPlayerMovement()
        ).Value();
        
        
        // Target facing Vector
        PhysicalBody weaponAnchor = new ComponentInObject<PhysicalBody>(
            gameObject,
            new NullPhysicalBody()
        ).Value();

        TargetLocationSource targetLocation = new ComponentInObject<TargetLocationSource>(
            new ParentOfGameObject(gameObject).Parent(),
            new NullTargetLocationSource()
        ).Value();
        
        Vector orientationVector = new Vector(
            new PointToPointVectorDefinition(
                weaponAnchor,
                targetLocation
            )
        );
        
        
        // Orientation
        weaponOrientation = new WeaponOrientation(
            weaponAnchor,
            orientationVector,
            spriteRenderer
        );
    }

    
    private void Start()
    {
        // Presentation - Hide if Player Rolls
        modelPresentation = new ConditionalVisibility(
            new SpriteVisibility(spriteRenderer),
            playerMovement.RollAction().Concluded()
        );
    }

    
    private void Update()
    {
        weaponOrientation.Orient();
        modelPresentation.Present();
    }
}