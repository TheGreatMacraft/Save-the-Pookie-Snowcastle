using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoTripper : GunBaseClass
{
    public float timeToKillNewEnemy;
    public int baseDamage;
    public int newMaxHealth;

    Coroutine resetCoroutine;

    private void Awake()
    {
        transform.parent.GetComponent<PlayerHealth>().maxHealth = newMaxHealth;
    }

    public override void Start()
    {
        base.Start();

        baseDamage = damage;
    }

    private void ResetTemporaryDamage()
    {
        damage = baseDamage;
    }

    public override void KilledEnemy()
    {
        // Cancel Reseting Damage after Time
        if (resetCoroutine != null)
            StopCoroutine(resetCoroutine);

        // Double damage, Start Resseting Damage after Time
        damage *= 2;
        resetCoroutine = StartCoroutine(ResetDamageAfterTime());
    }

    IEnumerator ResetDamageAfterTime()
    {
        yield return new WaitForSeconds(timeToKillNewEnemy);

        ResetTemporaryDamage();
    }
}
