using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Spawned new enemy!");
        EnemyTracker.instance.RegisterEnemy(gameObject);
    }
    public void DecreaseHelth(float health)
    {
        currentHealth -= health;
        if (currentHealth <= 0)
            Die();
    }

    public void IncreaseHealth(float health)
    {
        currentHealth += health;
    }

    private void Die()
    {
        EnemyTracker.instance.UnregisterEnemy(gameObject);
        Destroy(gameObject);
    }
}
