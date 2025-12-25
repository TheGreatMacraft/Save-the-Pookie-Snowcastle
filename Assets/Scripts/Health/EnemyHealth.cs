using UnityEngine;

public class EnemyHealth : HealthBase
{
    public override void Start()
    {
        base.Start();
        EnemyTracker.instance.Register(gameObject);
    }

    public override void Die()
    {
        Debug.Log("Killing " + this.gameObject.name);
        
        // Remove Enemy from Tracker and Self Destruct
        EnemyTracker.instance.Unregister(gameObject);
        Destroy(gameObject);
    }
}