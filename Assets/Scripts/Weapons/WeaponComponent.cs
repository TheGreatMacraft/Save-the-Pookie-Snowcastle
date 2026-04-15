using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public abstract class WeaponComponent :
    MonoBehaviour, WeaponActions
{
    [Header("Target Tag")]
    [SerializeField] protected string targetTag;
    
    [Header("Sprite Renderer")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected Clock coroutineClock;
    private Orientation weaponOrientation;
    private Perspective weaponPerspective;
    
    
    protected virtual void Awake()
    {
        coroutineClock = new CoroutineClock(this);

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
        
        weaponOrientation = new WeaponOrientation(
            weaponAnchor,
            orientationVector
        );

        weaponPerspective = new WeaponPerspective(
            spriteRenderer,
            orientationVector
        );
    }

    private void Update()
    {
        weaponOrientation.Orient();
        weaponPerspective.SetDepth();
    }
    
    
    protected ActionExecution primaryAction = new NullActionExecution();
    protected ActionExecution secondaryAction = new NullActionExecution();
    protected ActionExecution supportAction = new NullActionExecution();
    protected ActionExecution abilityAction = new NullActionExecution();
    
    public ActionExecution PrimaryAction()
        => primaryAction;
    public ActionExecution SecondaryAction()
        => secondaryAction;
    public ActionExecution SupportAction()
        => supportAction;
    public ActionExecution AbilityAction()
        => abilityAction;
}