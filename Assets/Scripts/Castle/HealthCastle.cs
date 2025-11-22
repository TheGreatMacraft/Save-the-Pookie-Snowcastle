using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCastle : MonoBehaviour
{
    public static HealthCastle Instance;

    public Slider healthBarSlider;

    public float currentHealth;
    public float maxHealth = 1000;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetHealthBar()
    {
        healthBarSlider.value = currentHealth / maxHealth;
    }

    public void DecreaseHelth(float health)
    {
        currentHealth -= health;
        SetHealthBar();

    }

    public void IncreaseHealth(float health)
    {
        currentHealth += health;
        SetHealthBar();
    }
}
