using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public abstract class WeaponComponent :
    MonoBehaviour, WeaponActions
{
    [Header("Target Tag")]
    [SerializeField] protected string targetTag;

    protected Clock coroutineClock;
    private Orientation weaponOrientation;
    
    
    protected virtual void Awake()
    {
        coroutineClock = new CoroutineClock(this);
        
        weaponOrientation = new WeaponOrientation(
            new ComponentInObject<PhysicalBody>(
                gameObject,
                new NullPhysicalBody()
                ).Value(),
            new ComponentInObject<TargetLocationSource>(
                new ParentOfGameObject(gameObject).Parent(),
                new NullTargetLocationSource()
                ).Value()
        );
    }

    private void Update()
    {
        weaponOrientation.Orient();
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