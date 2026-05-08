using UnityEngine;

public abstract class MeleeComponent
    : MonoBehaviour, WeaponActions
{
    [Header("Target")]
    [SerializeField] protected string targetTag;
    
    [Header("Attack Properties")]
    [SerializeField] private float slashCooldown;
    [SerializeField] private ColliderSensorComponent slashCollider;
    [SerializeField] private PhysicalBodyComponent slashSourceBody;
    
    protected Clock coroutineClock;
    
    private ActionExecution defaultAttack = new NullActionExecution();
    private ActionExecution heavyAttack = new NullActionExecution();
    private ActionExecution supportAction = new NullActionExecution();
    private ActionExecution ability = new NullActionExecution();
    
    
    protected virtual void Start()
    {
        coroutineClock = new CoroutineClock(this);
        
        Impact allImpact = new ActionImpacts(
            new AllComponentsInObject<Impact>(
                gameObject,
                new NullImpact()
            ).Value());

        defaultAttack = new ExecutionWithCooldown(
            new ConstantExecution(
                new SlashCall(
                    slashCollider,
                    new WeaponPayload(
                        targetTag,
                        allImpact,
                        new NullTerminable()
                    )
                )
            ),
            slashCooldown,
            coroutineClock,
            false
        );
    }

    public ActionExecution DefaultAttack() => defaultAttack;
    public ActionExecution SupportAction() => supportAction;
    public ActionExecution HeavyAttack() => heavyAttack;
    public ActionExecution Ability() => ability;
}