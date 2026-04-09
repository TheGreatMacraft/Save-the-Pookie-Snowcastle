using UnityEngine;


// Action System

public interface ActionExecution
{
    void Execute();
    bool Concluded();
}

public interface ActionCall
{
    void Call();
}

public interface ActionInterpreter
{
    void ExecuteActionCall();
}


// Impact on Enemy(Target)

public interface Impact
{
    void ApplyOn(GameObject target);
}