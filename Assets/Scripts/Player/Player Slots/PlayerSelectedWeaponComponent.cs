using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerSelectedWeaponComponent
    : MonoBehaviour, Scalar<Weapon>
{
    [SerializeField] private WeaponComponent[] weapons;

    private Index playerSlot;
    
    private InputActionStates inputActionStates;
    private ActionExecution slotSelection;


    private Index PlayerSlot()
        => playerSlot ??=
            new PlayerSlot(
                weapons.Length - 1
            );
    
    private InputActionStates InputActionStates()
        => inputActionStates ??=
            new InputActionStates(
                new ComponentInObject<PlayerInput>(
                    gameObject,
                    null
                ).Value()
            );
    

    private ActionExecution SlotSelection()
        => slotSelection ??=
            new MultipleActionExecutions(
                new ConditionalExecution(
                    new ConstantExecution(
                        new SimpleActionCall(() =>
                        {
                            PlayerSlot().SetTo(0);
                        })
                    ),
                    new OnPressed(
                        InputActionStates().PrimarySlotActionState()
                    )
                ),
                
                new ConditionalExecution(
                    new ConstantExecution(
                        new SimpleActionCall(() =>
                            {
                                PlayerSlot().SetTo(1);
                            }
                        )
                    ),
                    new OnPressed(
                        InputActionStates().SecondarySlotActionState()
                    )
                ),
                
                new ConditionalExecution(
                    new ConstantExecution(
                        new SimpleActionCall(() =>
                            {
                                PlayerSlot().SetTo(0);
                            }
                        )
                    ),
                    new OnPressed(
                        InputActionStates().TertirarySlotActionState()
                    )
                )
            );


    public Weapon Value() 
        => weapons[PlayerSlot().Value()];

    private void Update()
    {
        SlotSelection().Execute();
    }
}