using UnityEngine;

public sealed class PlayerStateComponent
    : MonoBehaviour, PlayerState
{
    private State currentState = new NullState();
    private State battleState = new BattleState();
    private State buildState = new BuildState();
    
    private void Awake()
    {
        SetState(battleState);
    }

    
    public State CurrentState() => currentState;
    
    public void SetState(State state)
    {
        currentState = state;
    }

    public void Toggle()
    {
        if(currentState.Equals(battleState))
            SetState(buildState);
        else
            SetState(battleState);
    }
}