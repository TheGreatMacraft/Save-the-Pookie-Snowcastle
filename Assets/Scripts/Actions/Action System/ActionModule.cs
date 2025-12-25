using System;

// Helper Class
[System.Serializable]
public class ActionModuleConfig
{
    public string name;
    public float cooldown;
    public bool callAfterCooldown;
}

// Main Module Class
public class ActionModule
{
    public bool canAct = true;
    public float cooldown;
    public bool callActionAfterCooldown;

    public Action action;
    public Action onFinished = null;

    public Func<bool> cancelCallOverride;

    public ActionModule( float cooldown, bool callActionAfterCooldown, Action action)
    {
        this.cooldown = cooldown;
        this.callActionAfterCooldown = callActionAfterCooldown;
        this.action = action;
    }
    
    public void ActionCall()
    {
        // Cancel If Can't Act
        if(!canAct
           || CancelCall()
           ) {return;}
        
        // Toggle Act bool after Cooldown and call Action
        if (callActionAfterCooldown)
        {
            Utils.ToggleValueInTime(
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
        Utils.ToggleValueInTime(
            v => canAct = v,
            canAct,
            false,
            cooldown);
    }

    protected virtual bool CancelCall()
    {
        if (cancelCallOverride != null)
            return cancelCallOverride.Invoke();
        
        return false;
    }

}