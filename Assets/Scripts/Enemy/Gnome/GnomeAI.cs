using UnityEngine;

public class GnomeAI : EnemyAIBase
{
    protected override bool ShouldChangeCurrentTarget(GameObject value)
    {
        return (base.ShouldChangeCurrentTarget(value) || !(ActiveBuildingTracker.instance.registeredElements.Contains(value)));
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

        if (attackScript.actionHandler is GnomeActions gnomeActionHandler)
        {
            BuildingBase targetBuilding = currentTarget.GetComponent<BuildingBase>();
            
            gnomeActionHandler.targetBuilding = targetBuilding;
            gnomeActionHandler.actionModules["Disarm"].cooldown = targetBuilding.timeToDisable;
        }
    }
}