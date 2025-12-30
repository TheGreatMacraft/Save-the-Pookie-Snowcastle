using System;

public class GnomeActions : ActionHandler
{
    public GnomeAI gnomeAI;
    
    public BuildingBase targetBuilding;

    protected override void Awake()
    {
        base.Awake();
        
        if(gnomeAI == null)
            gnomeAI = GetComponent<GnomeAI>();
    }

    protected void Disarm(BuildingBase buildingBase)
    {
        buildingBase.isEnabled = false;
    }
    
    protected override void RegisterActions()
    {
        base.RegisterActions();
        
        actions["Disarm"] = () => Disarm(targetBuilding);
    }
}