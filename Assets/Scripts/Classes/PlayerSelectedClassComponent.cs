using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[RequireComponent(typeof(PlayerStateComponent))]
[DisallowMultipleComponent]


public sealed class PlayerSelectedClassComponent
    : MonoBehaviour,Class
{
    [Header("Overall")]
    [SerializeField] private PlayerInput playerInput;
    
    [Header("Builder")]
    [SerializeField] private HologramComponent hologram;
    [SerializeField] private BuildingComponent buildingPrefab;
    
    [SerializeField] private ClassType selectedClassType;
    
    private Clock coroutineClock;
    private InputActionStates inputActionStates;
    private PlayerState playerState;
    
    private Class selectedClass;


    private Clock CoroutineClock()
        => coroutineClock ??= new CoroutineClock(this);

    private InputActionStates InputActionStates()
        => inputActionStates ??= new InputActionStates(playerInput);

    private PlayerState PlayerState()
        => playerState ??=
            new ComponentInObject<PlayerState>(
                gameObject,
                new NullPlayerState()
            ).Value();
    
    
    private Class SelectClass()
    {
        switch (selectedClassType)
        {
            case ClassType.Flanker:
                return new FlankerClass();
            
            case ClassType.Engineer:
                return new EngineerClass(
                    hologram,
                    new ComponentInObject<PhysicalBody>(
                        hologram,
                        new NullPhysicalBody()
                    ).Value(),
                    buildingPrefab,
                    PlayerState(),
                    CoroutineClock(),
                    InputActionStates()
                );
            
            default:
                return new NullClass();
        }
    }

    private Class SelectedClass()
        => selectedClass ??= 
            SelectClass();

    
    // Proxy
    public ReadOnlyCollection<ActionExecution> Abilities()
        => SelectedClass().Abilities();

    public float DefaultSpeedMultiplier()
        => SelectedClass().DefaultSpeedMultiplier();
    public float SprintSpeedMultiplier()
        => SelectedClass().SprintSpeedMultiplier();
}

public enum ClassType
{
    Flanker,
    Engineer
}