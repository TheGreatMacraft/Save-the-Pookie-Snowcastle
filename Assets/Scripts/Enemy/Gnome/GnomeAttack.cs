
public class GnomeAttack : EnemyAttackBase
{
    protected override void SetupComponents()
    {
        base.SetupComponents();
        
        attackAction = actionHandler.actionModules["Disarm"];
    }
    
    protected override void CustomActionLogic(ActionHandler attackActions)
    {
        attackActions.actionModules["Disarm"].ActionCall();
    }
}