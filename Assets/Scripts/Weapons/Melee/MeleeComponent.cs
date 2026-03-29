using UnityEngine;
[RequireComponent(typeof(WeaponOrientationComponent))]
[DisallowMultipleComponent]

public abstract class MeleeComponent:
    WeaponComponent
{
    [Header("Attack Properties")]
    [SerializeField] private float slashCooldown;
    [SerializeField] private ColliderSensorComponent slashCollider;
    [SerializeField] private PhysicalBodyComponent slashSource;
    
    private ActionExecution slashAction;
    
    
    protected virtual void Awake()
    {
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

        actionInterpreter = new InputInterpreter(
            slashAction,
            nullActionExecution,
            abilityAction,
            inputSystem
            );
    }

    private void Update()
    {
        actionInterpreter.ExecuteActionCalls();
    } 
}