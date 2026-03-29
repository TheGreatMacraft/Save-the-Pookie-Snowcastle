using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    public float actionRange;
    
    // Helper Class - allows the creating of ActionModule Dict from Inspector data
    [SerializeField] protected List<ActionModuleConfig> moduleConfigs = new List<ActionModuleConfig>();

    public Dictionary<string, ActionModule> actionModules = new();

    protected List<string> actionNames = new List<string>();
    public string mainActionName;

    protected virtual void Reset()
    {
        UpdateActionNameList();
        AddActionModuleConfigs(actionNames);
    }

    protected virtual void UpdateActionNameList()
    {
        actionNames.Clear();
    }

    protected void AddActionModuleConfigs(List<string> names)
    {
        foreach (string name in names)
        {
            moduleConfigs.Add(new ActionModuleConfig(name));
        }
    }
    
    protected virtual void Awake()
    {
        AddActionModules();

        if (actionModules.Count == 1)
            mainActionName = actionModules.Keys.First();
        
    }

    private void AddActionModules()
    {
        foreach (var config in moduleConfigs)
        {
            actionModules[config.name] = new ActionModule(config,Utils.GetActionByName(this,config.name));
        }
    }

    // How the actions should be executed -> primarily for Entity use
    public virtual void ActionExecutionOrder() {}
}