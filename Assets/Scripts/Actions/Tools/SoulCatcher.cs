
using UnityEngine;

public class SoulCatcher : ToolBase
{
    // Assigned in Inspector
    public float duration;
    
    // External Necessary Objects
    public AttackActions weapon;
    
    // Used in Script
    private int killedEnemiesUsed = 0;
    private int baseWeaponDamage;

    protected override void SetupComponents()
    {
        // Weapon
        if(weapon == null)
            weapon = transform.parent.gameObject.GetComponentInChildren<AttackActions>();
        
        // Base Damage
        baseWeaponDamage = weapon.hitEssentials.damageAmount;
        
        // Cancel Tool Call Condition
        actionModules["Tool"].cancelCallOverride = () =>
        {
            return weapon.enemiesKilled == killedEnemiesUsed;
        };

    }

    protected override void Tool()
    {
        Debug.Log("Soul Catcher");
        int newKills = weapon.enemiesKilled - killedEnemiesUsed;
        int buffedDamage = baseWeaponDamage + newKills * 5; 
        
        Utils.ToggleValueInTime<int>(
            v => weapon.hitEssentials.damageAmount = v,
            baseWeaponDamage,
            buffedDamage,
            duration);

        killedEnemiesUsed = weapon.enemiesKilled;
    }
}