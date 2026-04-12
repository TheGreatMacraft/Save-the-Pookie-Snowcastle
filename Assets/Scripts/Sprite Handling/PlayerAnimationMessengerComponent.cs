using UnityEngine;
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]

public sealed class PlayerAnimationMessengerComponent
    : MonoBehaviour, PlayerAnimationMessenger
{
    private Animator playerAnimator;
    
    private void Awake()
    {
        playerAnimator = new ComponentInObject<Animator>(
            gameObject,
            null
        ).Value();
    }

    public void ToggleWalking(bool value)
    {
        playerAnimator.SetBool("isRunning", value);
    }

    public void TriggerRolling()
    {
        playerAnimator.SetTrigger("triggerRoll");
    }
}