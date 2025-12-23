using System.Collections;
using UnityEngine;

public class EgoTripper : GunActionsBase
{
    // Variables to be Assigned in Inspector
    public float timeToKillNewEnemy;
    public int baseDamage;
    public int newMaxHealth;

    private Coroutine resetCoroutine;

    protected override void Awake()
    {
        base.Awake();
        
        SetupComponents();
    }

    private void SetupComponents()
    {
        // Set Max Health to New Max Health
        transform.parent.GetComponent<PlayerHealth>().maxHealth = newMaxHealth;
        
        // Set the Base Damage
        baseDamage = hitEssentials.damageAmount;
    }

    public override void KilledEnemy()
    {
        // Cancel Reseting Damage after Time
        if (resetCoroutine != null)
            StopCoroutine(resetCoroutine);

        // Double damage, Start Resseting Damage after Time
        hitEssentials.damageAmount *= 2;
        resetCoroutine = StartCoroutine(ResetDamageAfterTime());
    }

    private IEnumerator ResetDamageAfterTime()
    {
        yield return new WaitForSeconds(timeToKillNewEnemy);

        ResetTemporaryDamage();
    }
    
    private void ResetTemporaryDamage()
    {
        hitEssentials.damageAmount = baseDamage;
    }
}