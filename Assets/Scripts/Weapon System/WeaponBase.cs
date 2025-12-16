using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class WeaponActionModuleConfig
{
    public string name;
    public float cooldown;
    public bool callAfterCooldown;
}

public abstract class WeaponBase : MunitionBase
{
    [SerializeField] private List<WeaponActionModuleConfig> moduleConfigs;
    
    public Dictionary<string, WeaponActionModule> actionModules = new();

    protected abstract Dictionary<string, Action> WeaponActionFunctions();

    protected virtual void Awake()
    {
        var actions = WeaponActionFunctions();
        
        foreach (var config in moduleConfigs)
        {
            if (actions.TryGetValue(config.name, out var action))
            {
                actionModules[config.name] = new WeaponActionModule(
                config.cooldown,
                config.callAfterCooldown,
                action);
            }
        }
    }
    
    // Virtual Method, called Upon Killing Enemy, to be Defined in Derived Class
    public virtual void KilledEnemy() {}
}