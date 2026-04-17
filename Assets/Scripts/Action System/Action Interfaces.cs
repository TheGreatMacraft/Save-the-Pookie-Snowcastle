using UnityEngine;


// Action System

public interface ActionExecution : Condition
{
    void Execute();
    Condition Concluded() => this;
}

public interface ActionCall
{
    void Call();
}

// Impact on Enemy(Target)

public interface Impact
{
    void ApplyOn(GameObject target);
}