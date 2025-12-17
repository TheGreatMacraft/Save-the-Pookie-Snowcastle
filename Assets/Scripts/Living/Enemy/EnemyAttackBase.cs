using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    // External Objects Necessary
    public EnemyAIBase AIBaseScript;
    
    // Variables used in Script
    public WeaponBase weapon;
    protected void Start()
    {
        SetupComponents();
    }

    protected void SetupComponents()
    {
        // AI Base Script
        if (AIBaseScript == null)
            AIBaseScript = GetComponent<EnemyAIBase>();
        
        // Weapon Script
        weapon = GetComponentInChildren<WeaponBase>();
    }

    protected void Update()
    {
        // Cancel if:
        if(AIBaseScript.currentTarget == null   // Target is Null
           || AIBaseScript.currentState != EnemyState.Attacking // Current State isn't Attacking
           ) {return;}
        
        if(weapon is GunBase gunWeapon)
            GunLogic(gunWeapon);
        
        else if(weapon is MeleeBase meleeWeapon)
            MeleeLogic(meleeWeapon);
    }

    protected void GunLogic(GunBase gunWeapon)
    {
        // If can Attack
        gunWeapon.actionModules["Attack"].ActionCall();
        
        // If magazine empty reload
        if(gunWeapon.NeedsReloading())
            gunWeapon.actionModules["Reload"].ActionCall();
    }
    
    protected void MeleeLogic(MeleeBase meleeWeapon)
    {
        
    }
}