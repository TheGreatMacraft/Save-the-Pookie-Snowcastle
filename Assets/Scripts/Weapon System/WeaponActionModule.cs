using System;
using UnityEngine;

public class WeaponActionModule
{
    public bool canAct = true;
    public float cooldown;
    public bool callActionAfterCooldown;
    public Action action;

    public WeaponActionModule( float cooldown, bool callActionAfterCooldown, Action action)
    {
        this.cooldown = cooldown;
        this.callActionAfterCooldown = callActionAfterCooldown;
        this.action = action;
    }
    
    public void ActionCall()
    {
        // Cancel If Can't Act
        if(!canAct) {return;}
        
        // Toggle Act bool after Cooldown and call Action
        if (callActionAfterCooldown)
        {
            Utils.ToggleBoolInTime(
                v => canAct = v,
                canAct,
                cooldown,
                action);
            
            return;
        }
        
        // Call Action
        action();
        
        // Toggle Act bool in Cooldown
        Utils.ToggleBoolInTime(
            v => canAct = v,
            canAct,
            cooldown);
    }
}