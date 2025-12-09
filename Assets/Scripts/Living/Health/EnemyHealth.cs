using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public override void Start()
    {
        base.Start();
        EnemyTracker.instance.Register(gameObject);
    }

    public override void Die(WeaponBaseClass weapon = null)
    {
        // Notify Weapon an Enemy was Killed
        if(weapon != null)
        {
            weapon.KilledEnemy();
        }

        // Remove Enemy from Tracker and Self Destruct
        EnemyTracker.instance.Unregister(gameObject);
        Destroy(gameObject);
    }
}
