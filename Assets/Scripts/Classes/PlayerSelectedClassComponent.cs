using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[RequireComponent(typeof(PlayerStateComponent))]
[DisallowMultipleComponent]


public sealed class PlayerSelectedClassComponent
    : MonoBehaviour, Class, Scalar<Class>
{
    [SerializeField] private HologramComponent hologram;

    [SerializeField] private ClassType selectedClassType;
    private List<Scalar<Class>> selectedClass = new(1);


    public Class Value()
    {
        if (selectedClass.Count == 0)
        {
            selectedClass.Add(
                new SelectedClass(
                    selectedClassType,
                    new ComponentInObject<PlayerState>(
                        gameObject,
                        new NullPlayerState()
                    ).Value(),
                    hologram
                )
            );
        }

        return selectedClass[0].Value();
    }

    
    // Proxy
    public ReadOnlyCollection<ActionExecution> Abilities()
        => Value().Abilities();

    public float DefaultSpeedMultiplier()
        => Value().DefaultSpeedMultiplier();
    public float SprintSpeedMultiplier()
        => Value().SprintSpeedMultiplier();
}

public enum ClassType
{
    Flanker,
    Engineer
}