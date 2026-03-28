using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class HealthComponent : MonoBehaviour, Health
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider slider; 
    [SerializeField] private MonoBehaviour deathProvider;
    
    private Health health;
    private HealthReporter reporter;
    
    private Media UISlider; // Optional
    private MortalityMonitor vitals;
    

    private void Awake()
    {
        if (deathProvider is Mortal death)
        {
            var healthValue = new HealthValue(maxHealth);
            health = healthValue;
            reporter = healthValue;
            
            UISlider = slider != null
                ? new UISlider(slider)
                : new NullUISlider();
            
            vitals = new Vitals(death);
        }
        else
            throw new Exception("Death Provider doesn't implement Mortal");
    }

    private void Update()
    {
        reporter.Report(UISlider,vitals);
    }
    
    
    // Proxy
    public void TakeDamage(float damage) => health.TakeDamage(damage);
    public void Heal(float heal) => health.Heal(heal);
}