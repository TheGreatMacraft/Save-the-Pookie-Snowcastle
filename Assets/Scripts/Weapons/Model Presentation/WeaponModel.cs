using UnityEngine;

public sealed class WeaponModel
    : Presentation
{
    private readonly ActionExecution weaponOrientation;
    private readonly ActionExecution modelPresentation;


    public WeaponModel(
        SpriteRenderer spriteRenderer,
        PhysicalBody weaponAnchor,
        TargetLocation targetLocation,
        PlayerMovement playerMovement,
        PlayerState playerState,
        Condition isWeaponSelected
    )
        : this(
            new WeaponOrientation(
                weaponAnchor,
                new Vector(
                    new PointToPointVectorDefinition(
                        weaponAnchor,
                        targetLocation
                    )
                ),
                spriteRenderer
            ),
            new SpriteVisibility(spriteRenderer),
            new AndConditions(
                playerMovement.RollAction().Concluded(),
                new OrConditions(
                    new IsIdentityCondition<State>(
                        playerState, new BattleState()
                    ),
                    new IsIdentityCondition<State>(
                        playerState, new NullState()
                    )
                ),
                isWeaponSelected
            ),
            targetLocation.IsTargetFound()
        ) {}

    private WeaponModel(
         Orientation weaponOrientation,
         Togglable spriteVisibility,
         Condition weaponVisible,
         Condition targetFound
    )
    : this(
        new ConditionalExecution(
            new ConstantExecution(
                new SimpleActionCall(() => weaponOrientation.Orient())
            ),
            new AndConditions(
                weaponVisible,
                targetFound
            )
        ),
        new OnChangeExecution(
            new ConstantExecution(
                new SimpleActionCall(() => spriteVisibility.Toggle())
            ),
            weaponVisible
        )
    ) {}

    private WeaponModel(
        ActionExecution weaponOrientation,
        ActionExecution modelPresentation
        )
    {
        this.weaponOrientation = weaponOrientation;
        this.modelPresentation = modelPresentation;
    }


    public void Present()
    {
        weaponOrientation.Execute();
        modelPresentation.Execute();
    }
}