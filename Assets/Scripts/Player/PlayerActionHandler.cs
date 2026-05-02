using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovementComponent))]
[DisallowMultipleComponent]


public sealed class PlayerActionHandler : MonoBehaviour
{
    [SerializeField] private GunComponent gun;

    private readonly List<ActionExecution> weaponActions = new(1);
    private readonly List<ActionExecution> stateToggler = new(1);
    private readonly List<ActionExecution> classAbilities = new(1);
    
    private readonly List<InputActionStates> inputActionStates = new(1);
    private readonly List<PlayerMovement> playerMovement = new(1);
    private readonly List<PlayerState> playerState = new(1);
    

    private InputActionStates InputActionStates()
    {
        if (inputActionStates.Count == 0)
        {
            inputActionStates.Add(new InputActionStates(
                new ComponentInObject<PlayerInput>(gameObject, null).Value()
            ));
        }
        return inputActionStates[0];
    }

    private PlayerMovement PlayerMovement()
    {
        if (playerMovement.Count == 0)
        {
            playerMovement.Add(new ComponentInObject<PlayerMovement>(
                gameObject, 
                new NullPlayerMovement()
            ).Value());
        }
        return playerMovement[0];
    }

    private PlayerState PlayerState()
    {
        if (playerState.Count == 0)
        {
            playerState.Add(new ComponentInObject<PlayerState>(
                gameObject, 
                new NullPlayerState()
            ).Value());
        }
        return playerState[0];
    }

    private Condition IsBattleState() => new IsStateCondition(PlayerState(), new BattleState());
    

    private ActionExecution WeaponActions()
    {
        if (weaponActions.Count == 0)
        {
            weaponActions.Add(new MultipleActionExecutions(
                // Default Attack
                new ConditionalExecution(
                    gun.DefaultAttack(),
                    new AndConditions(
                        new DefaultAttackInputCondition(InputActionStates()),
                        PlayerMovement().RollAction().Concluded(),
                        IsBattleState()
                    )
                ),
                // Heavy Attack - NOT WORKING YET 
                new ConditionalExecution(
                    new ChargedActionExecution(
                        new NullActionCall(), 
                        new NullActionCall(), 
                        gun.HeavyAttack(),
                        0f, 
                        new CoroutineClock(this),
                        new ChargeHeavyAttackInputCondition(InputActionStates())
                    ),
                    new AndConditions(
                        new StartHeavyAttackInputCondition(InputActionStates()),
                        IsBattleState()
                    )
                ),
                // Support Action
                new ConditionalExecution(
                    gun.SupportAction(),
                    new AndConditions(
                        new SupportActionInputCondition(InputActionStates()),
                        IsBattleState()
                    )
                ),
                // Ability Action
                new ConditionalExecution(
                    gun.Ability(),
                    new AndConditions(
                        new AbilityInputCondition(InputActionStates()),
                        IsBattleState()
                    )
                )
            ));
        }
        return weaponActions[0];
    }

    private ActionExecution ClassAbilities()
    {
        if (classAbilities.Count == 0)
        {
            Class selectedClass = new ComponentInObject<Class>(gameObject, new NullClass()).Value();
            classAbilities.Add(new MultipleActionExecutions(selectedClass.Abilities()));
        }
        return classAbilities[0];
    }

    private ActionExecution StateToggler()
    {
        if (stateToggler.Count == 0)
        {
            stateToggler.Add(new OnTrueExecution(
                new ToggleCall(PlayerState()),
                new BuildMenuInputCondition(InputActionStates())
            ));
        }
        return stateToggler[0];
    }
    

    private void Update()
    {
        ClassAbilities().Execute();
        WeaponActions().Execute();
        StateToggler().Execute();
    }
}