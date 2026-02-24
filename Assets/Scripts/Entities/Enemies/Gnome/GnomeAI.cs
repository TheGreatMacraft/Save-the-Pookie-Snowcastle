using UnityEngine;

public class GnomeAI : EntityAIBase
{
    protected override bool ShouldChangeCurrentTarget()
    {
        return (base.ShouldChangeCurrentTarget() || !(ActiveBuildingTracker.instance.registeredElements.Contains(currentTarget)));
    }

    protected override void Update()
    {
        if(!ActiveBuildingTracker.instance.anyElementsRegistred()) {return;}
        base.Update();
    }

    protected override GameObject FindNewTarget()
    {
        float minDistance = float.MaxValue;
        GameObject currentPick = null;
        
        foreach (var defense in ActiveBuildingTracker.instance.registeredElements)
        {
            float distanceToDefense = Vector3.Distance(transform.position, defense.transform.position);
            if (minDistance > distanceToDefense)
            {
                minDistance = distanceToDefense;
                currentPick = defense;
            }
        }
        
        return currentPick;
    }

    public override void SetNewTarget(GameObject target = null)
    {
        base.SetNewTarget(target);

        if (currentTarget == null) { return;}

        if (actScript.actionHandler is GnomeActions gnomeActionHandler)
        {
            BuildingBase targetBuilding = currentTarget.GetComponent<BuildingBase>();
            
            gnomeActionHandler.actionModules["Disarm"].cooldown = targetBuilding.timeToDisable;
        }
    }
}