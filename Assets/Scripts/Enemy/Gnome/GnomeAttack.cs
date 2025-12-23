using UnityEngine;

public class GnomeAttack : EnemyAttackBase
{
    protected override void CustomActionLogic(ActionHandler attackActions)
    {
        attackActions.actionModules["Disarm"].ActionCall();
    }
}