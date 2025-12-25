using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBase : MonoBehaviour
{
    // UI Slider (Optional)
    public Slider healthBarSlider;

    // Current Health - When Changed Calls OnHealthChanged()
    public float _currentHealth;

    public float maxHealth;

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

    // Events for Notifying Other Scripts
    public event Action Hurt;
    public event Action Heal;

    public void InitialiseHealth()
    {
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float health, AttackActions attackingweapon = null)
    {
        currentHealth -= health;

        // Check if Object should Die
        if (currentHealth <= 0)
            DieWithWeapon(attackingweapon);

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
        if (healthBarSlider != null)
            healthBarSlider.value = currentHealth / maxHealth;
    }

    // Virtual Methods for Derived Classes to Use
    public void DieWithWeapon([CanBeNull] AttackActions attackingweapon = null)
    {
        // Notify Weapon an Enemy was Killed
        if (attackingweapon != null)
            attackingweapon.enemiesKilled++;

        Die();
    }
    
    public virtual void Die() {}
}