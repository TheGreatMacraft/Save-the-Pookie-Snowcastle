using System;
using UnityEngine;

public class EnemyWeaponContoller : MonoBehaviour
{
    private WeaponBase weapon;

    private void Awake()
    {
        SetupComponents();
    }

    private void SetupComponents()
    {
        weapon = GetComponentInChildren<WeaponBase>();
    }

    // Weapon Controller Interface Methods
    public void HandleAttack()
    {
        
    }
    public void HandleReload() => weapon.actionModules["Reload"].ActionCall();
    public void HandleAbility() => weapon.actionModules["Ability"].ActionCall();
}