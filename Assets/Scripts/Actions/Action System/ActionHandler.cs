using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    public float actionRange;
    
    [SerializeField] private List<ActionModuleConfig> moduleConfigs;

    public Dictionary<string, ActionModule> actionModules = new();
    
    protected Dictionary<string, Action> actions = new();
    

    protected virtual void Awake()
    {
        RegisterActions();
        
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
    
    protected virtual void RegisterActions() {}
}