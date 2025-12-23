public class EnemyHealth : HealthBase
{
    public override void Start()
    {
        base.Start();
        EnemyTracker.instance.Register(gameObject);
    }

    public override void Die(AttackActions attackingweapon = null)
    {
        // Notify Weapon an Enemy was Killed
        if (attackingweapon != null) attackingweapon.KilledEnemy();

        // Remove Enemy from Tracker and Self Destruct
        EnemyTracker.instance.Unregister(gameObject);
        Destroy(gameObject);
    }
}