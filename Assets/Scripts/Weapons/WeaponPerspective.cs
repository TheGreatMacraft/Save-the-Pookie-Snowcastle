using UnityEngine;

public sealed class WeaponPerspective
    : Perspective
{
    private readonly SpriteRenderer spriteRenderer;
    private readonly Vector weaponOrientation;


    public WeaponPerspective(
        SpriteRenderer spriteRenderer,
        Vector weaponOrientation
        )
    {
        this.spriteRenderer = spriteRenderer;
        this.weaponOrientation = weaponOrientation;
    }


    public void SetDepth()
    {
        float angle = weaponOrientation.AngleInDegrees();

        spriteRenderer.sortingOrder = angle > 0
            ? 2
            : 3;
    }
}