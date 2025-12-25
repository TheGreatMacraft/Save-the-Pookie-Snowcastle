using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    // External Objects Necessary
    public EnemyAIBase AIBaseScript;
    
    // Variables used in Script
    public ActionHandler actionHandler;
    public ActionModule attackAction;
    
    protected void Start()
    {
        SetupComponents();
    }

    protected virtual void SetupComponents()
    {
        // AI Base Script
        AIBaseScript ??= GetComponent<EnemyAIBase>();
        
        // Actions Script
        actionHandler = GetComponentInChildren<ActionHandler>();
        
        // Attack Finish Action
        if (actionHandler.actionModules.TryGetValue("Attack", out var attackAction))
        {
            attackAction.onFinished = AIBaseScript.UpdateCurrentState;
            this.attackAction = attackAction;
        }
    }

    protected void Update()
    {
        // Cancel if:
        if(AIBaseScript.currentTarget == null   // Target is Null
           || AIBaseScript.currentState != EnemyState.Attacking // Current State isn't Attacking
           ) {return;}

        // Run Gun Logic if the Attack Component is a Gun
        if (actionHandler is GunActionsBase gunActions)
        {
            GunAttackLogic(gunActions);
        }
        
        // Run Melee Logic if the Attack Component is a Melee Weapon
        else if (actionHandler is MeleeActionsBase meleeActions)
        {
            MeleeAttackLogic(meleeActions);
        }

        // Custom Logic if Attack Component is Unique
        else
        {
            CustomActionLogic(actionHandler);
        }
    }

    protected virtual void GunAttackLogic(GunActionsBase gunActions)
    {
        gunActions.actionModules["Attack"].ActionCall();
        
        if(gunActions.NeedsReloading())
            gunActions.actionModules["Reload"].ActionCall();

        if(gunActions.actionModules.TryGetValue("Ability", out var abilityAction))
            abilityAction.ActionCall();
    }

    protected virtual void MeleeAttackLogic(MeleeActionsBase meleeActions)
    {
        meleeActions.actionModules["Attack"].ActionCall();
        
        if(actionHandler.actionModules.TryGetValue("Ability", out var abilityAction))
            abilityAction.ActionCall();
    }

    protected virtual void CustomActionLogic(ActionHandler actionHandler)
    {
        
    }
}