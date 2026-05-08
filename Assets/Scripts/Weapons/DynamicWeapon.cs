public sealed class DynamicWeapon : Weapon
{
    private readonly ActionExecution defaultAttack;
    private readonly ActionExecution supportAction;
    private readonly ActionExecution heavyAttack;
    private readonly ActionExecution ability;
    private readonly Presentation weaponPresentation;

    public DynamicWeapon(Scalar<Weapon> source)
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
        ), 
        new ProxyPresentation(
            () => source.Value()
        )
    ) {}

    private DynamicWeapon(
        ActionExecution defaultAttack,
        ActionExecution supportAction,
        ActionExecution heavyAttack,
        ActionExecution ability,
        Presentation weaponPresentation
    )
    {
        this.defaultAttack = defaultAttack;
        this.supportAction = supportAction;
        this.heavyAttack = heavyAttack;
        this.ability = ability;
        this.weaponPresentation = weaponPresentation;
    }


    public ActionExecution DefaultAttack()
        => defaultAttack;

    public ActionExecution SupportAction()
        => supportAction;

    public ActionExecution HeavyAttack()
        => heavyAttack;

    public ActionExecution Ability()
        => ability;

    public void Present()
        => weaponPresentation.Present();
}