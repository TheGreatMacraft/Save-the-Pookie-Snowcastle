using System;
using UnityEngine;

// Helper Class
[System.Serializable]
public class ActionModuleConfig
{
    public string name;
    public float cooldown;
    public bool callAfterCooldown;
    public ActionModuleConfig(string name)
    {
        this.name = name;
    }
}

// Main Module Class
[System.Serializable]
public class ActionModule
{
    // Assigned in Inspector
    public float cooldown;
    public bool callActionAfterCooldown;
    
    // Used in Script
    [NonSerialized] public bool canAct = true;
    [NonSerialized] public Action action;
    
    [NonSerialized] public Coroutine actionCoroutine;

    public ActionModule(ActionModuleConfig config, Action action)
    {
        this.cooldown = config.cooldown;
        this.callActionAfterCooldown = config.callAfterCooldown;
        this.action = action;
    }
    
    public void ActionCall()
    {
        // Cancel If Can't Act
        if(!canAct) {return;}

        // Toggle Act bool after Cooldown and call Action
        if (callActionAfterCooldown)
        {
            actionCoroutine = Utils.ToggleValueInTime(
                v => canAct = v,
                canAct,
                false,
                cooldown,
                () =>
                {
                    action?.Invoke();
                }
                );
        }
        else
        {
            // Call Action
            action?.Invoke();
        
            // Toggle Act bool in Cooldown
            actionCoroutine = Utils.ToggleValueInTime(
                v => canAct = v,
                canAct,
                false,
                cooldown);
        }
    }
}