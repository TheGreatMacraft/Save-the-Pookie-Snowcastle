using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCastle : BaseHealth<HealthCastle>
{
    public static HealthCastle Instance;

    public Slider healthBarSlider;

    public override void OnHealthChanged()
    {
        healthBarSlider.value = currentHealth / maxHealth;
    }
    public override void Die()
    {
        //Game Over
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        InitialiseHealth();
    }
}
