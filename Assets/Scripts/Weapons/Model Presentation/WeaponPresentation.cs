using UnityEngine;

public sealed class WeaponPresentation:
    WeaponModel
{
    private readonly Visibility spriteVisibility;
    private readonly Orientation weaponOrientation;
    private readonly Presentation modelPresentation;


    public WeaponPresentation(
        SpriteRenderer spriteRenderer,
        PhysicalBody weaponAnchor,
        Location targetLocation,
        PlayerMovement playerMovement,
        PlayerState playerState
    )
        : this(
            new SpriteVisibility(spriteRenderer),
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
            new ConditionalVisibility(
                new SpriteVisibility(spriteRenderer),
                new AndConditions(
                    playerMovement.RollAction().Concluded(),
                    new IsIdentityCondition<State>(
                        playerState, new BattleState()
                    )
                )
            )
        ) {}

    private WeaponPresentation(
        Visibility spriteVisibility,
        Orientation weaponOrientation,
        Presentation modelPresentation
    )
    {
        this.spriteVisibility = spriteVisibility;
        this.weaponOrientation = weaponOrientation;
        this.modelPresentation = modelPresentation;
    }


    public void Present()
    {
        weaponOrientation.Orient();
        modelPresentation.Present();
    }

    public void Show() => spriteVisibility.Show();
    public void Hide() => spriteVisibility.Hide();
    public bool IsMet() => spriteVisibility.IsMet();
}