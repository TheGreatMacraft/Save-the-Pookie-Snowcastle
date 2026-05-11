using UnityEngine;

public sealed class WeaponModel
    : Presentation
{
    private readonly ActionExecution weaponOrientation;
    private readonly ActionExecution modelPresentation;


    public WeaponModel(
        SpriteRenderer spriteRenderer,
        PhysicalBody weaponAnchor,
        Location targetLocation,
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
            new AndConditions(
                playerMovement.RollAction().Concluded(),
                new IsIdentityCondition<State>(
                    playerState, new BattleState()
                ),
                isWeaponSelected
            ),
            new SpriteVisibility(spriteRenderer)
        ) {}

    private WeaponModel(
         Orientation weaponOrientation,
         Condition weaponVisible,
         Togglable spriteVisibility
    )
    : this(
        new ConditionalExecution(
            new ConstantExecution(
                new SimpleActionCall(() => weaponOrientation.Orient())
            ),
            weaponVisible
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