using System.Diagnostics;
using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[RequireComponent(typeof(PlayerStateComponent))]
[DisallowMultipleComponent]


public sealed class PlayerSelectedClassComponent
    : MonoBehaviour,Class
{
    [SerializeField] private HologramComponent hologram;
    [SerializeField] private ClassType selectedClassType;
    
    private PlayerState playerState;
    
    private Class selectedClass;


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
                return new EngineerClass(hologram, PlayerState());
            
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