public class AttackActions : ActionHandler
{
    public HitEssentials hitEssentials;

    protected override void Awake()
    {
        base.Awake();
        
        hitEssentials = GetComponent<HitEssentials>();
    }
    
    protected override void RegisterActions()
    {
        actions["Attack"] = Attack;
    }

    protected virtual void Attack() {}
    
    // Virtual Method, called Upon Killing Enemy, to be Defined in Derived Class
    public virtual void KilledEnemy() {}
}