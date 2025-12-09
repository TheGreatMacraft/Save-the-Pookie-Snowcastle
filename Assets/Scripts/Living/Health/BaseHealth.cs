using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseHealth: MonoBehaviour
{
    // Events for Notifying Other Scripts
    public event Action Hurt;
    public event Action Heal;

    // UI Slider (Optional)
    public Slider healthBarSlider;

    // Current Health - When Changed Calls OnHealthChanged()
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

    public virtual void Start()
    {
        InitialiseHealth();
    }

    public float maxHealth;

    public void InitialiseHealth()
    {
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float health, WeaponBaseClass weapon = null)
    {
        currentHealth -= health;

        // Check if Object should Die
        if (currentHealth <= 0)
            Die(weapon);

        // Call Hurt Event
        Hurt?.Invoke();
    }

    public void IncreaseHealth(float health)
    {
        if (currentHealth + health >= maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += health;

        // Call Heal Event
        Heal?.Invoke();
    }

    public virtual void OnHealthChanged()
    {
        // Update UI Slider
        if(healthBarSlider != null)
            healthBarSlider.value = currentHealth / maxHealth;
    }

    // Virtual Methods for Derived Classes to Use
    public virtual void Die(WeaponBaseClass weapon = null) { }
    public virtual void Die() { }
}
