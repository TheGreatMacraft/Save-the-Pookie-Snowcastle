using UnityEngine;

public sealed class PlayerStateComponent
    : MonoBehaviour, PlayerState
{
    private State currentState = new NullState();
    private State battleState = new BattleState();
    private State buildState = new BuildState();
    
    private void Awake()
    {
        TransitionTo(battleState);
    }

    
    public State Current() => currentState;
    
    public void TransitionTo(State state)
    {
        currentState = state;
    }

    public void Toggle()
    {
        if(currentState.Equals(battleState))
            TransitionTo(buildState);
        else
            TransitionTo(battleState);
    }
}