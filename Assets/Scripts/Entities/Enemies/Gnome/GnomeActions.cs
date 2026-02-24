public class GnomeActions : ActionHandler
{
    public GnomeAI gnomeAI;
    
    protected override void UpdateActionNameList()
    {
        base.UpdateActionNameList();
        
        mainActionName = "Disarm";
        
        actionNames.Add("Disarm");
    }

    protected void Disarm()
    {
        BuildingBase buildingBase = gnomeAI.currentTarget.GetComponent<BuildingBase>();
        buildingBase.isEnabled = false;
    }

    public override void ActionExecutionOrder()
    {
        actionModules["Disarm"].ActionCall();
    }
}