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
        Force targetForce = new ComponentInObject<Force>(
                targetGameObject,
                new NullForce()
            ).Value();
            
        targetForce.Stun(
            knockbackAmount/15
            );
        targetForce.AddImpulse(
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