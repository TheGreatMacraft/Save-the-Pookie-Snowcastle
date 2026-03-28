using System.Collections.Generic;
using UnityEngine;

public class MeleeActionsBase : AttackActions
{
    // Variables used in Script
    public Transform attackPoint;

    protected override void UpdateActionNameList()
    {
        base.UpdateActionNameList();
        
        mainActionName = "Slash";
        
        actionNames.Add("Slash");
    }

    protected override void SetupComponents()
    {
        base.SetupComponents();
        
        // Attack Point
        if(attackPoint  == null)
            attackPoint = transform;
    }

    private void Slash()
    {
        // Get all Opponents In Range
        GameObject[] opponentsInRange = 
            Utils.GetObjectsInRadiousWithTag(attackPoint.position, actionRange, hitEssentials.affectedObjectsTag);

        // Hit Every Enemy In Range
        HitInRadious(opponentsInRange);
        
        // Play Swing Animation
    }

    public void HitInRadious(GameObject[] opponentsInRange)
    {
        foreach (var opponent in opponentsInRange)
            MeleeComponent.HitTarget(
                opponent,
                transform.position,
                this,
                hitEssentials
            );
    }

    public override void ActionExecutionOrder()
    {
        actionModules["Slash"].ActionCall();
    }
}