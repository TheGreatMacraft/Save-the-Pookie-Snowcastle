using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerMovement))]
[DisallowMultipleComponent]

public sealed class PlayerInputDirector 
    : MonoBehaviour 
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private WeaponComponent weapon;
    
    private ActionInterpreter playerActions;
    
    private ActionExecution heavyAttack = new  NullActionExecution();

    private PlayerMovement playerMovement;

    private void Start()
    {
        // Movement
        playerMovement = new ComponentInObject<PlayerMovement>(
            gameObject,
            new NullPlayerMovement()
        ).Value();
        
        ActionExecution rollAction = playerMovement.RollAction();
        
        // Weapon
        ActionExecution defaultAttack = weapon.PrimaryAction();
        ActionExecution secondaryAction = weapon.SecondaryAction();
        ActionExecution supportAction = weapon.SupportAction();
        ActionExecution weaponAbility = weapon.AbilityAction();
        
        InputActionStates inputActionStates 
            = new InputActionStates(playerInput);
        
        playerActions = new AllActionsInterpreter(
            new SimpleReadOnlyCollection<ActionInterpreter>(
                
                // Default Attack
                new InputActionLink(
                    defaultAttack,
                    new InputTriggerCombo(
                        new SimpleReadOnlyCollection<InputTrigger>(
                            new OnPressed(
                                inputActionStates.PrimaryActionState()
                            ),
                            new OnBeingReleased(
                                inputActionStates.PowerActionState()
                            )
                        )
                    )
                ),
                
                // Secondary Action
                new InputActionLink(
                    secondaryAction,
                    new OnPressed(
                        inputActionStates.SecondaryActionState()
                    )
                ),
                
                // Support Action
                new InputActionLink(
                    supportAction,
                    new OnPressed(
                        inputActionStates.SupportActionState()
                    )
                ),
                
                // Ability
                new InputActionLink(
                    weaponAbility,
                    new OnPressed(
                        inputActionStates.SpecialActionState()
                    )
                ),
                
                // Heavy Attack
                new InputActionLink(
                    heavyAttack,
                    new InputTriggerCombo(
                        new SimpleReadOnlyCollection<InputTrigger>(
                            new OnPressed(
                                inputActionStates.PrimaryActionState()
                            ),
                            new OnBeingPressed(
                                inputActionStates.PowerActionState()
                            )
                        )
                    )
                ),
                
                // Roll
                new InputActionLink(
                    rollAction,
                    new OnPressed(
                        inputActionStates.MovementActionState()
                        )
                )
            )
        );
    }


    private void Update()
    {
        playerActions.ExecuteActionCall();
    }
}