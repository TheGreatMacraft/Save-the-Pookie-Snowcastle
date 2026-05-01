using System.Collections.Generic;
using UnityEngine.InputSystem;

public sealed class InputActionStates
{
    private readonly PlayerInput playerInput;

    private List<InputActionState> primaryActionState = new(1);
    private List<InputActionState> secondaryActionState = new(1);
    
    private List<InputActionState> movementActionState = new(1);
    private List<InputActionState> movementSpecialActionState = new(1);
    private List<InputActionState> powerActionState = new(1);
    
    private List<InputActionState> specialActionState = new(1);
    private List<InputActionState> supportActionState = new(1);
    private List<InputActionState> interactActionState = new(1);
    
    private List<InputActionState> buildMenuActionState = new(1);

    
    public InputActionStates(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }


    public InputActionState PrimaryActionState()
    {
        if (primaryActionState.Count == 0)
        {
            primaryActionState.Add(
                new InputActionState(
                    playerInput,
                    new PrimaryInputAction()
                )
            );
        }
        
        return primaryActionState[0];
    }

    public InputActionState SecondaryActionState()
    {
        if (secondaryActionState.Count == 0)
        {
            secondaryActionState.Add(
                new InputActionState(
                    playerInput,
                    new SecondaryInputAction()
                )
            );
        }

        return secondaryActionState[0];
    }

    public InputActionState MovementActionState()
    {
        if (movementActionState.Count == 0)
        {
            movementActionState.Add(
                new InputActionState(
                    playerInput,
                    new MovementInputAction()
                )
            );
        }

        return movementActionState[0];
    }
    
    public InputActionState MovementSpecialActionState()
    {
        if (movementSpecialActionState.Count == 0)
        {
            movementSpecialActionState.Add(
                new InputActionState(
                    playerInput,
                    new MovementSpecialInputAction()
                )
            );
        }

        return movementSpecialActionState[0];
    }

    public InputActionState PowerActionState()
    {
        if (powerActionState.Count == 0)
        {
            powerActionState.Add(
                new InputActionState(
                    playerInput,
                    new PowerInputAction()
                )
            );
        }

        return powerActionState[0];
    }

    public InputActionState SpecialActionState()
    {
        if (specialActionState.Count == 0)
        {
            specialActionState.Add(
                new InputActionState(
                    playerInput,
                    new SpecialInputAction()
                )
            );
        }

        return specialActionState[0];
    }

    public InputActionState SupportActionState()
    {
        if (supportActionState.Count == 0)
        {
            supportActionState.Add(
                new InputActionState(
                    playerInput,
                    new SupportInputAction()
                )
            );
        }

        return supportActionState[0];
    }

    public InputActionState InteractActionState()
    {
        if (interactActionState.Count == 0)
        {
            interactActionState.Add(
                new InputActionState(
                    playerInput,
                    new InteractInputAction()
                )
            );
        }

        return interactActionState[0];
    }
    
    public InputActionState BuildMenuActionState()
    {
        if (buildMenuActionState.Count == 0)
        {
            buildMenuActionState.Add(
                new InputActionState(
                    playerInput,
                    new BuildMenuInputAction()
                )
            );
        }

        return buildMenuActionState[0];
    }
}