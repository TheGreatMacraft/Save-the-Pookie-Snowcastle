using UnityEngine;
[RequireComponent(typeof(PlayerAnimationMessengerComponent))]
[DisallowMultipleComponent]

public sealed class PlayerAnimationControler
    : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimationMessenger playerAnimationMessenger;
    private ActionExecution triggerRolling;


    private PlayerMovement PlayerMovement()
        => playerMovement ??=
            new ComponentInObject<PlayerMovement>(
                new ParentOfGameObject(gameObject).Value(),
                new NullPlayerMovement()
            ).Value();
    
    private PlayerAnimationMessenger PlayerAnimationMessenger()
        => playerAnimationMessenger ??=
            new ComponentInObject<PlayerAnimationMessenger>(
                gameObject,
                new NullPlayerAnimationMessenger()
            ).Value();
    
    private ActionExecution TriggerRolling()
        => triggerRolling ??=
            new OnTrueExecution(
                new SimpleActionCall(
                    () => PlayerAnimationMessenger().TriggerRolling()
                ),
                PlayerMovement().RollAction().InProgress()
            );
    

    private void Update()
    {
        PlayerAnimationMessenger().ToggleWalking(
            PlayerMovement().Movement().InProgress().IsMet()
        );
        
        TriggerRolling().Execute();
    }
}