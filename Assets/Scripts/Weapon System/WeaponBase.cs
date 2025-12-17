using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MunitionBase
{
    public float weaponRange; // Used by Entities, as Minimal Distance for Attack
    
    [SerializeField] private List<ActionModuleConfig> moduleConfigs;
    
    public Dictionary<string, ActionModule> actionModules = new();

    protected abstract Dictionary<string, Action> WeaponActionFunctions();

    protected virtual void Awake()
    {
        var actions = WeaponActionFunctions();
        
        foreach (var config in moduleConfigs)
        {
            if (actions.TryGetValue(config.name, out var action))
            {
                actionModules[config.name] = new ActionModule(
                config.cooldown,
                config.callAfterCooldown,
                action);
            }
        }
    }
    
    // Virtual Method, called Upon Killing Enemy, to be Defined in Derived Class
    public virtual void KilledEnemy() {}
}