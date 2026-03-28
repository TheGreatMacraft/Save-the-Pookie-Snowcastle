using System;
using UnityEngine;

public sealed class HealthValue : Health, HealthReporter
{
    private readonly float maxHealth;
    private float currentHealth;
 

    public HealthValue(float maxHealth)
        : this(maxHealth, maxHealth) {}
    
    private HealthValue(float maxHealth, float currentHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
    }
    

    public void TakeDamage(float damage)
    {
        currentHealth = Math.Max(currentHealth - damage, 0);
    }

    public void Heal(float heal)
    {
        currentHealth = Math.Min(currentHealth + heal, maxHealth);
    }

    public void Report(Media media, MortalityMonitor monitor)
    {
        media.Update(currentHealth,maxHealth);
        monitor.Report(currentHealth);
    }
}