using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    // Assigned in Inspector
    private PlayerInput playerInput;
    
    private WeaponBase weapon;

    private void Start()
    {
        SetupComponents();
        
        SubscribeButtonsToEvents();
    }

    private void SetupComponents()
    {
        // Weapon Script
        weapon = GetComponentInChildren<WeaponBase>();
        
        // Player Input
        if(playerInput == null)
            playerInput = GetComponent<PlayerInput>();
    }

    private void SubscribeButtonsToEvents()
    {
        // Match Input Buttons to Action Calls
        playerInput.actions["AttackButton"].performed += ctx => weapon.actionModules["Attack"].ActionCall();
        playerInput.actions["ReloadButton"].performed += ctx => weapon.actionModules["Reload"].ActionCall();
        playerInput.actions["AbilityButton"].performed += ctx => weapon.actionModules["Ability"].ActionCall();
    }
}