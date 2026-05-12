public sealed class DynamicWeaponActions : WeaponActions
{
    private readonly ActionExecution defaultAttack;
    private readonly ActionExecution supportAction;
    private readonly ActionExecution heavyAttack;
    private readonly ActionExecution ability;

    
    public DynamicWeaponActions(Scalar<Weapon> source)
    : this(
        new ProxyActionExecution(
            () => source.Value().DefaultAttack()
        ),
        new ProxyActionExecution(
            () => source.Value().SupportAction()
        ),
        new ProxyActionExecution(
            () => source.Value().HeavyAttack()
        ),
        new ProxyActionExecution(
            () => source.Value().Ability()
        )
    ) {}

    private DynamicWeaponActions(
        ActionExecution defaultAttack,
        ActionExecution supportAction,
        ActionExecution heavyAttack,
        ActionExecution ability
    )
    {
        this.defaultAttack = defaultAttack;
        this.supportAction = supportAction;
        this.heavyAttack = heavyAttack;
        this.ability = ability;
    }


    public ActionExecution DefaultAttack()
        => defaultAttack;

    public ActionExecution SupportAction()
        => supportAction;

    public ActionExecution HeavyAttack()
        => heavyAttack;

    public ActionExecution Ability()
        => ability;
}