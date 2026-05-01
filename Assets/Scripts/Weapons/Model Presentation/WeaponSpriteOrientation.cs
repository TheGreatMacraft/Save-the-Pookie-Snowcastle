using UnityEngine;

public sealed class WeaponSpriteOrientation
    : Orientation
{
    private readonly Perspective spritePerspective;
    private readonly Orientation spriteOrientation;


    public WeaponSpriteOrientation(
        SpriteRenderer spriteRenderer,
        Vector orientationVector
    )
        : this(
            new WeaponPerspective(
                spriteRenderer,
                orientationVector
            ),
            new SpriteYOrientation(
                spriteRenderer,
                orientationVector
            )
        ) {}
    
    private WeaponSpriteOrientation(
        Perspective spritePerspective,
        Orientation spriteOrientation
    )
    {
        this.spritePerspective = spritePerspective;
        this.spriteOrientation = spriteOrientation;
    }


    public void Orient()
    {
        spriteOrientation.Orient();
        spritePerspective.SetDepth();
    }
}