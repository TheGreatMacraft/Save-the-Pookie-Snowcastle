using System;

public sealed class ProxyActionExecution : ActionExecution
{
    private readonly Func<ActionExecution> source;
    
    
    public ProxyActionExecution(Func<ActionExecution> source)
    {
        this.source = source;
    }
    
    
    public void Execute() => source().Execute();
    public Condition InProgress() => source().InProgress();
    public Condition Concluded() => source().Concluded();
}