using UnityEngine;
[RequireComponent(typeof(PlayerAnimationMessengerComponent))]
[DisallowMultipleComponent]

public sealed class PlayerAnimationControler
    : MonoBehaviour
{
    [SerializeField] private PlayerMovementComponent playerMovement;
    
    private PlayerAnimationMessenger playerAnimationMessenger;
    private ActionExecution triggerRolling;

    
    private void Awake()
    {
        playerAnimationMessenger = new ComponentInObject<PlayerAnimationMessenger>(
            gameObject,
            new NullPlayerAnimationMessenger()
        ).Value();

        triggerRolling = new OnTrueExecution(
            new SimpleActionCall(
                () => playerAnimationMessenger.TriggerRolling()
                ),
            playerMovement.RollAction().InProgress()
        );
    }

    private void FixedUpdate()
    {
        playerAnimationMessenger.ToggleWalking(playerMovement.IsMoving().IsMet());
        triggerRolling.Execute();
    }
}