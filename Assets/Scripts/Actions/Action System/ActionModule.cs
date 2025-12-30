using System;
using UnityEngine;

// Helper Class
[System.Serializable]
public class ActionModuleConfig
{
    public string name;
    public float cooldown;
    public bool callAfterCooldown;
    public bool cancelWithButton;
}

// Main Module Class
public class ActionModule
{
    public bool canAct = true;
    public float cooldown;
    public bool callActionAfterCooldown;
    public bool cancelWithButton;

    public Coroutine actionCoroutine;
    
    public Action action;
    public Action onFinished = null;

    public Func<bool> cancelCallOverride;
    public Action cancelCallAftermath;

    public ActionModule( float cooldown, bool callActionAfterCooldown, bool cancelWithButton, Action action)
    {
        this.cooldown = cooldown;
        this.callActionAfterCooldown = callActionAfterCooldown;
        this.cancelWithButton = cancelWithButton;
        this.action = action;
    }
    
    public void ActionCall()
    {
        if (cancelWithButton && !canAct)
            CancelCall();
            
        // Cancel If Can't Act
        if(!canAct
           || CheckIfCancelCall()
           ) {return;}
        
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
                    onFinished?.Invoke();
                }
                );
            
            return;
        }
        
        // Call Action
        action?.Invoke();
        
        // Optional Action, called at the end of Attack
        onFinished?.Invoke();
        
        // Toggle Act bool in Cooldown
        actionCoroutine = Utils.ToggleValueInTime(
            v => canAct = v,
            canAct,
            false,
            cooldown);
    }

    protected virtual bool CheckIfCancelCall()
    {
        if (cancelCallOverride != null)
            return cancelCallOverride.Invoke();
        
        return false;
    }

    protected virtual void CancelCall()
    {
        cancelCallAftermath?.Invoke();
    }
}