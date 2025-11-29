using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth<EnemyHealth>
{
    private void Start()
    {
        InitialiseHealth();
        EnemyTracker.instance.Register(gameObject);
    }

    public override void Die()
    {
        EnemyTracker.instance.Unregister(gameObject);
        Destroy(gameObject);
    }
}
