using UnityEngine;

public abstract class MeleeComponent:
    WeaponComponent
{
    [Header("Attack Properties")]
    [SerializeField] private float slashCooldown;
    [SerializeField] private ColliderSensorComponent slashCollider;
    [SerializeField] private PhysicalBodyComponent slashSourceBody;
    
    protected void Start()
    {
        Impact allImpact = new ActionImpacts(
            new AllComponentsInObject<Impact>(
                gameObject,
                new NullImpact()
            ).Value());
        
        defaultAttackAction = new InstantAction(
            new SlashCall(
                slashCollider,
                new WeaponPayload(
                    targetTag,
                    allImpact,
                    new NullTerminable()
                    )
                ),
            slashCooldown,
            new MultipleConditions(
                new DefaultAttackInputCondition(inputActionStates),
                playerMovement.RollConcluded()
                ),
            coroutineClock
        );
    }
}