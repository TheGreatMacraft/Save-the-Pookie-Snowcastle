using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]

public sealed class PlayerAnimationMessengerComponent
    : MonoBehaviour, PlayerAnimationMessenger
{
    private List<Animator> playerAnimator = new(1);
    
    
    private Animator PlayerAnimator()
    {
        if (playerAnimator.Count == 0)
        {
            playerAnimator.Add(
                new ComponentInObject<Animator>(
                    gameObject,
                    null
                ).Value()
            );
        }
        
        return playerAnimator[0];
    }

    
    public void ToggleWalking(bool value)
    {
        PlayerAnimator().SetBool("isRunning", value);
    }

    public void TriggerRolling()
    {
        PlayerAnimator().SetTrigger("triggerRoll");
    }
}