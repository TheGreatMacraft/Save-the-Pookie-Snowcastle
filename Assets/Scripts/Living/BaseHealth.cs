using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth<H> : MonoBehaviour where H : BaseHealth<H>
{
    public event Action Hurt;
    public event Action Heal;

    public float _currentHealth;
    public float currentHealth
    {
        get => _currentHealth;
        protected set
        {
            _currentHealth = value;
            OnHealthChanged();
        }
    }

    public float maxHealth;

    public void InitialiseHealth()
    {
        currentHealth = maxHealth;
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
        if (currentHealth + health >= maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += health;

        Heal?.Invoke();
    }

    public virtual void OnHealthChanged() { }
    public virtual void Die() { }
}
