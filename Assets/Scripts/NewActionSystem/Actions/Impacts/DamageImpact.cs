using UnityEngine;

public sealed class DamageImpact : MonoBehaviour, Impact
{
    [SerializeField] private int damageAmount;
    

    public void ApplyOn(GameObject target)
    {
        new ComponentInObject<Damageable>(
            target,
            new NullDamageable()
            ).Value()
            .TakeDamage(damageAmount);
    }
}