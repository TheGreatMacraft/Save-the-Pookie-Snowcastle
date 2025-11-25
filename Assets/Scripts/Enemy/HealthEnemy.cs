using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public event Action Hurt;

    public float currentHealth;
    public float maxHealth = 100f;

    private void Start()
    {
        currentHealth = maxHealth;
        EnemyTracker.instance.Register(gameObject);
    }

    public void DecreaseHelth(float health)
    {
        currentHealth -= health;
        if (currentHealth <= 0)
            Die();

        Hurt?.Invoke();
    }

    public void IncreaseHealth(float health)
    {
        currentHealth += health;
    }

    private void Die()
    {
        EnemyTracker.instance.Unregister(gameObject);
        Destroy(gameObject);
    }
}
