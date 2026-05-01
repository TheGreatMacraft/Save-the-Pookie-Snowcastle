using System;

public sealed class SimpleActionCall : ActionCall
{
    private readonly Action action;


    public SimpleActionCall(Action action)
    {
        this.action = action;
    }
    

    public void Call()
    {
        action?.Invoke();
    }
}