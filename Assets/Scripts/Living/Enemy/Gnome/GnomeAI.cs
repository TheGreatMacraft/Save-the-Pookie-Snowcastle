using UnityEngine;

public class GnomeAI : EnemyAIBase
{
    protected override GameObject FindNewTarget()
    {
        float minDistance = float.MaxValue;
        GameObject currentPick = null;
        
        foreach (var defense in ActiveBuildingTracker.instance.registeredElements)
        {
            float distanceToDefense = Vector3.Distance(transform.position, defense.transform.position);
            if (minDistance > distanceToDefense)
            {
                distanceToDefense = minDistance;
                currentPick = defense;
            }
        }

        return currentPick;
    }
}