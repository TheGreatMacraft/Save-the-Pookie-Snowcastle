using UnityEngine;


// Action System

public interface ActionExecution
{
    public void Execute();
    public Condition Concluded();
    public Condition InProgress();
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