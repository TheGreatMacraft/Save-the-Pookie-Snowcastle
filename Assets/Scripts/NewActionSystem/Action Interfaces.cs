using UnityEngine;


// Action System

public interface ActionExecution
{
    void Execute();
}

public interface ActionCall
{
    void Call();
}

public interface ActionInterpreter
{
    void ExecuteActionCalls();
}


// Impact on Enemy(Target)

public interface Impact
{
    void ApplyOn(GameObject target);
}