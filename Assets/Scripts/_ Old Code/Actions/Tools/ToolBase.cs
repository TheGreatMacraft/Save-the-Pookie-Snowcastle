public class ToolBase : ActionHandler
{
    protected override void UpdateActionNameList()
    {
        base.UpdateActionNameList();
        
        actionNames.Add("Tool");
    }

    protected override void Awake()
    {
        base.Awake();
        SetupComponents();
    }
    
    protected virtual void SetupComponents() {}
    
    
    protected virtual void Tool() {}
}