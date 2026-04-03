using UnityEngine;
[RequireComponent(typeof(WeaponOrientationComponent))]
[DisallowMultipleComponent]

public abstract class MeleeComponent:
    WeaponComponent
{
    [Header("Attack Properties")]
    [SerializeField] private float slashCooldown;
    [SerializeField] private ColliderSensorComponent slashCollider;
    [SerializeField] private PhysicalBodyComponent slashSourceBody;

    private ActionInterpreter meleeInterpreter;
    private ActionExecution slashAction;
    
    protected virtual void Awake()
    {
        base.Awake();
        
        Impact allImpact = new ActionImpacts(
            new AllComponentsInObject<Impact>(
                gameObject,
                new NullImpact()
            ).Value());
        
        slashAction = new InstantAction(
            new SlashCall(
                slashCollider,
                new WeaponPayload(
                    targetTag,
                    allImpact,
                    new NullTerminable()
                    )
                ),
            slashCooldown,
            coroutineClock
        );

        meleeInterpreter = new AllActionsInterpreter(
            new SimpleReadOnlyCollection<ActionInterpreter>(
                new InputActionLink(
                    slashAction,
                    new OnPressed(
                        new InputActionState(
                            playerInput,
                            new PrimaryInputAction()
                            )
                        )
                    )
                )
            );
    }

    protected override void Update()
    {
        base.Update();
        
        meleeInterpreter.ExecuteActionCall();
    } 
}