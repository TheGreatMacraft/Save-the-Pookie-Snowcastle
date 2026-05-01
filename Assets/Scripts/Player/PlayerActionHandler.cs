using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovementComponent))]
[DisallowMultipleComponent]

public sealed class PlayerActionHandler
    : MonoBehaviour
{
    [SerializeField] private GunComponent gun;
    
    private ActionExecution weaponActions;
    private ActionExecution stateToggler;
    private ActionExecution classAbilities;

    
    private void Start()
    {
        // Input
        PlayerInput playerInput = new ComponentInObject<PlayerInput>(
            gameObject,
            null
        ).Value();

        InputActionStates inputActionStates 
            = new InputActionStates(playerInput);
        
        
        // Player Movement
        PlayerMovement playerMovement = new ComponentInObject<PlayerMovement>(
            gameObject,
            new NullPlayerMovement()
        ).Value();
        
        
        // Player State
        PlayerState playerState = new ComponentInObject<PlayerState>(
            gameObject,
            new NullPlayerState()
        ).Value();
        
        Condition isBattleState = new IsStateCondition(playerState, new BattleState());
        
        
        // Weapon
        Weapon weapon = gun;
        
        
        // Selected Class
        Class selectedClass = new ComponentInObject<Class>(
            gameObject,
            new NullClass()
        ).Value();
        

        // Binding Actions
        weaponActions = new MultipleActionExecutions(
            
            // Default Attack
            new ConditionalExecution(
                weapon.DefaultAttack(),
                new MultipleConditions(
                    new DefaultAttackInputCondition(inputActionStates),
                    playerMovement.RollAction().Concluded(),
                    isBattleState
                )
            ),
            
            // Heavy Attack - NOT WORKING YET
            new ConditionalExecution(
                new ChargedActionExecution(
                    new NullActionCall(), //onStartHeavyAttackCharge
                    new NullActionCall(), //onCancelHeavyAttackCharge
                    weapon.HeavyAttack(),
                    0f, //heavyAttackChargeTime
                    new  CoroutineClock(this),
                    new ChargeHeavyAttackInputCondition(inputActionStates)
                ),
                new MultipleConditions(
                    new StartHeavyAttackInputCondition(inputActionStates),
                    isBattleState
                )
            ),
            
            // Support Action
            new ConditionalExecution(
                weapon.SupportAction(),
                new MultipleConditions(
                    new SupportActionInputCondition(inputActionStates),
                    isBattleState
                )
            ),
            
            // Ability Action
            new ConditionalExecution(
                weapon.Ability(),
                new MultipleConditions(
                    new AbilityInputCondition(inputActionStates),
                    isBattleState
                )
            )
        );

        classAbilities = new MultipleActionExecutions(
            selectedClass.Abilities()
        );

        stateToggler = new OnTrueExecution(
            new ToggleCall(playerState),
            new BuildMenuInputCondition(inputActionStates)
        );
    }

    private void Update()
    {
        classAbilities.Execute();
        weaponActions.Execute();
        stateToggler.Execute();
    }
}