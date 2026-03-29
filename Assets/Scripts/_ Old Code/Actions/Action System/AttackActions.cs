using UnityEngine;

[RequireComponent(typeof(HitEssentials))]
public class AttackActions : ActionHandler
{
    public HitEssentials hitEssentials;

    public int _enemiesKilled = 0;
    
    public int enemiesKilled
    {
        get => _enemiesKilled;
        set
        {
            if (value == _enemiesKilled + 1)
                OnKilledEnemy();
            _enemiesKilled = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SetupComponents();
    }
    
    protected virtual void SetupComponents()
    {
        // Hit Essentials
        if(hitEssentials  == null)
            hitEssentials = GetComponent<HitEssentials>();
    }
    
    // Virtual Method, called Upon Killing Enemy, to be Defined in Derived Class
    public virtual void OnKilledEnemy() {}
}