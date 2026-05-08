using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovementComponent))]
[RequireComponent(typeof(PlayerStateComponent))]
[RequireComponent(typeof(PlayerSelectedWeaponComponent))]
[DisallowMultipleComponent]


public sealed class PlayerActionHandler : MonoBehaviour
{
    private InputActionStates inputActionStates;
    private PlayerMovement playerMovement;
    
    private PlayerState playerState;
    private Condition isBattleState;
    
    private Weapon selectedWeapon;

    private Presentation weaponPresentation;
    
    private ActionExecution weaponActions;
    private ActionExecution stateToggler;
    private ActionExecution classAbilities;
    

    // Input
    private InputActionStates InputActionStates()
        => inputActionStates ??=
            new InputActionStates(
                new ComponentInObject<PlayerInput>(
                    gameObject, 
                    null
                ).Value()
            );

    // Movement
    private PlayerMovement PlayerMovement()
        => playerMovement ??=
            new ComponentInObject<PlayerMovement>(
                gameObject,
                new NullPlayerMovement()
            ).Value();

    // Player State
    private PlayerState PlayerState()
        => playerState ??=
            new ComponentInObject<PlayerState>(
                gameObject,
                new NullPlayerState()
            ).Value();

    private Condition IsBattleState() 
        => isBattleState ??=
            new IsIdentityCondition<State>(
            PlayerState(), 
            new BattleState()
        );
    
    
    
    // Selected Weapon
    private Weapon SelectedWeapon()
        => selectedWeapon ??=
            new DynamicWeapon(
                new ComponentInObject<Scalar<Weapon>>(
                    gameObject,
                    new NullScalar<Weapon>(new NullWeapon())
                ).Value()
            );
    
    
    // Actions
    private ActionExecution WeaponActions()
        => weaponActions ??=
            new MultipleActionExecutions(
                // Default Attack
                new ConditionalExecution(
                    SelectedWeapon().DefaultAttack(),
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
                        SelectedWeapon().HeavyAttack(),
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
                    SelectedWeapon().SupportAction(),
                    new AndConditions(
                        new SupportActionInputCondition(InputActionStates()),
                        IsBattleState()
                    )
                ),
                // Ability Action
                new ConditionalExecution(
                    SelectedWeapon().Ability(),
                    new AndConditions(
                        new AbilityInputCondition(InputActionStates()),
                        IsBattleState()
                    )
                )
            );

    private ActionExecution ClassAbilities()
        => classAbilities ??=
            new MultipleActionExecutions(
                new ComponentInObject<Class>(
                    gameObject, 
                    new NullClass()
                ).Value().Abilities()
            );

    private ActionExecution StateToggler()
        => stateToggler ??=
            new OnTrueExecution(
                new ConstantExecution(
                    new ToggleCall(PlayerState()
                    )
                ),
                new BuildMenuInputCondition(InputActionStates())
            );
    

    private void Update()
    {
        ClassAbilities().Execute();
        
        WeaponActions().Execute();
        SelectedWeapon().Present();
        
        StateToggler().Execute();
    }
}