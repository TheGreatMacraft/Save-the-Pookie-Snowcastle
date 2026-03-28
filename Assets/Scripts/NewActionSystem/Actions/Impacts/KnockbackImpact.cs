using UnityEngine;
[RequireComponent(typeof(PhysicalBody))]
[DisallowMultipleComponent]


public sealed class KnockbackImpact : 
    MonoBehaviour, 
    Impact
{
    [SerializeField] private float knockbackAmount;
    private PhysicalBody body;

    private void Awake()
    {
        body = new ComponentInObject<PhysicalBody>(
            gameObject,
            null
            ).Value();
    }

    public void ApplyOn(GameObject targetGameObject)
    {
        new ComponentInObject<Force>(
                targetGameObject,
                new NullForce()
            ).Value()
            .AddImpulse(
                new Vector(
                    new ConstantVectorDefinition(
                        body,
                        new ComponentInObject<Location>(
                            targetGameObject,
                            new NullLocation()
                        ).Value()
                    )
                ),
                knockbackAmount
            );
    }
}