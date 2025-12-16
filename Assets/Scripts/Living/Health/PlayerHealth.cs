using UnityEngine;

public class PlayerHealth : HealthBase
{
    public override void Die()
    {
        gameObject.transform.position = new Vector3(-3f, 0f, 0f);
        InitialiseHealth();
        HealthCastle.Instance.DecreaseHealth(HealthCastle.Instance.maxHealth / 8);
    }
}