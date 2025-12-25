using System;
using System.Collections.Generic;

public class ToolBase : ActionHandler
{
    protected virtual void Start()
    {
        SetupComponents();
        
        ExecuteOnStart();
    }
    
    protected virtual void SetupComponents() {}

    protected virtual void ExecuteOnStart() {}
    
    protected virtual void Tool() {}
    
    protected override void RegisterActions()
    {
        base.RegisterActions();
        
        actions["Tool"] = Tool;
    }
    
}