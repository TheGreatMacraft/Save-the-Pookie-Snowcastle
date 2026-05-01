public sealed class NullWeapon : Weapon
{
    private readonly ActionExecution nullAction = new NullActionExecution();

    public ActionExecution DefaultAttack() => nullAction;
    public ActionExecution HeavyAttack() => nullAction;
    public ActionExecution SupportAction() => nullAction;
    public ActionExecution Ability() => nullAction;
}