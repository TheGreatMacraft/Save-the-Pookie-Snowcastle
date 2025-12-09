using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public abstract class WeaponBaseClass : MonoBehaviour
{
    // Input Action Variables
    public PlayerInput playerInput;

    public virtual void Start()
    {
        SetButtonFunctions();
    }

    // Linking Buttons to Method Calls
    public virtual void SetButtonFunctions()
    {
        if (playerInput == null)
        {
            playerInput = transform.parent.GetComponent<PlayerInput>();
        }

        var shootAction = playerInput.actions["AttackButton"];
        var abilityAction = playerInput.actions["AbilityButton"];

        shootAction.performed += AttackCall;
        shootAction.Enable();

        abilityAction.performed += AbilityCall;
        abilityAction.Enable();
    }

    // Virtual Method, called Upon Killing Enemy, to be Defined in Derived Class
    public virtual void KilledEnemy() { }

    // Virtual Method Calls for Input Action
    public virtual void AttackCall(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
    }

    public virtual void AbilityCall(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
    }
}
