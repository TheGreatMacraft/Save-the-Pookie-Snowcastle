using System.Collections.Generic;
using UnityEngine.InputSystem;

public sealed class InputActionStates
{
    private readonly PlayerInput playerInput;

    private InputActionState primaryActionState;
    private InputActionState secondaryActionState;
    
    private InputActionState movementActionState;
    private InputActionState movementSpecialActionState;
    private InputActionState powerActionState;
    
    private InputActionState specialActionState;
    private InputActionState supportActionState;
    private InputActionState interactActionState;
    
    private InputActionState switchStateActionState;
    
    private InputActionState primarySlotActionState;
    private InputActionState secondarySlotActionState;
    private InputActionState tertiarySlotActionState;

    
    public InputActionStates(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }


    public InputActionState PrimaryActionState()
        => primaryActionState ??=
            new InputActionState(
                playerInput,
                new PrimaryInputAction()
            );

    public InputActionState SecondaryActionState()
        => secondaryActionState ??=
            new InputActionState(
                playerInput,
                new SecondaryInputAction()
            );

    public InputActionState MovementActionState()
        => movementActionState ??=
            new InputActionState(
                playerInput,
                new MovementInputAction()
            );

    public InputActionState MovementSpecialActionState()
        => movementSpecialActionState ??=
            new InputActionState(
                playerInput,
                new MovementSpecialInputAction()
            );

    public InputActionState PowerActionState()
        => powerActionState ??=
            new InputActionState(
                playerInput,
                new PowerInputAction()
            );

    public InputActionState SpecialActionState()
        => specialActionState ??=
            new InputActionState(
                playerInput,
                new SpecialInputAction()
            );

    public InputActionState SupportActionState()
        => supportActionState ??=
            new InputActionState(
                playerInput,
                new SupportInputAction()
            );

    public InputActionState InteractActionState()
        => interactActionState ??=
            new InputActionState(
                playerInput,
                new InteractInputAction()
            );

    public InputActionState SwitchStateActionState()
        => switchStateActionState ??=
            new InputActionState(
                playerInput,
                new SwitchStateInputAction()
            );
    
    // Player Slots
    public InputActionState PrimarySlotActionState()
        =>  primarySlotActionState ??=
            new InputActionState(
                playerInput,
                new Slot1InputAction()
            );
    
    public InputActionState SecondarySlotActionState()
        =>  secondaryActionState ??=
            new InputActionState(
                playerInput,
                new Slot2InputAction()
            );
    
    public InputActionState TertirarySlotActionState()
        =>  tertiarySlotActionState ??=
            new InputActionState(
                playerInput,
                new Slot3InputAction()
            );
}