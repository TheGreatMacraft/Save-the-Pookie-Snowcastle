using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    // Assigned in Inspector
    private PlayerInput playerInput;
    
    private AttackActions attack;

    private void Start()
    {
        SetupComponents();
        
        SubscribeButtonsToEvents();
    }

    private void SetupComponents()
    {
        // Weapon Script
        attack = GetComponentInChildren<AttackActions>();
        
        // Player Input
        if(playerInput == null)
            playerInput = GetComponent<PlayerInput>();
    }

    private void SubscribeButtonsToEvents()
    {
        // Match Input Buttons to Action Calls
        playerInput.actions["AttackButton"].performed += ctx => attack.actionModules["Attack"].ActionCall();
        playerInput.actions["ReloadButton"].performed += ctx => attack.actionModules["Reload"].ActionCall();
        playerInput.actions["AbilityButton"].performed += ctx => attack.actionModules["Ability"].ActionCall();
    }
}