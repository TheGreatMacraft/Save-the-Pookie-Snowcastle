public sealed class NullWeapon : Weapon
{
    private readonly ActionExecution nullAction = new NullActionExecution();
    private readonly Condition falseCondition = new FalseCondition();

    public ActionExecution DefaultAttack() => nullAction;
    public ActionExecution HeavyAttack() => nullAction;
    public ActionExecution SupportAction() => nullAction;
    public ActionExecution Ability() => nullAction;
    
    public void Present() {}
    
    public void Show() {}
    public void Hide() {}
    public Condition IsVisible() => falseCondition;
}