using UnityEngine;

public abstract class MeleeComponent:
    WeaponComponent
{
    [Header("Attack Properties")]
    [SerializeField] private float slashCooldown;
    [SerializeField] private ColliderSensorComponent slashCollider;
    [SerializeField] private PhysicalBodyComponent slashSourceBody;
    
    protected virtual void Awake()
    {
        base.Awake();
        
        Impact allImpact = new ActionImpacts(
            new AllComponentsInObject<Impact>(
                gameObject,
                new NullImpact()
            ).Value());
        
        primaryAction = new InstantAction(
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
    }
}