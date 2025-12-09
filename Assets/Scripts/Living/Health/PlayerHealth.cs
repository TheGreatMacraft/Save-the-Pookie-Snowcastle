using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public override void Die()
    {
        this.gameObject.transform.position = new Vector3(-3f, 0f, 0f);
        InitialiseHealth();
        HealthCastle.Instance.DecreaseHealth(HealthCastle.Instance.maxHealth / 8);
    }
}
