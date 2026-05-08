using UnityEngine;

public sealed class WeaponPresentation:
   Presentation
{
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
        Orientation weaponOrientation,
        Presentation modelPresentation
    )
    {
        this.weaponOrientation = weaponOrientation;
        this.modelPresentation = modelPresentation;
    }
    
    
    public void Present()
    {
        weaponOrientation.Orient();
        modelPresentation.Present();
    }
}