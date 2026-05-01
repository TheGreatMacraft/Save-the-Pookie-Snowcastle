using UnityEngine;

public sealed class WeaponOrientation : 
    Orientation
{
    private readonly Rotatable weaponRotatable;
    private readonly Rotation rotation;
    private readonly Orientation spriteOrientation;

    public WeaponOrientation(
        PhysicalBody weaponAnchor,
        Location targetLocation,
        SpriteRenderer spriteRenderer
    )
        : this(
            weaponAnchor,
            new Vector(
                new PointToPointVectorDefinition(
                    weaponAnchor,
                    targetLocation
                )
            ),
            spriteRenderer
        ) {}
    
    public WeaponOrientation(
        Rotatable weaponAnchor,
        Vector orientationVector,
        SpriteRenderer spriteRenderer
        )
        : this(
            weaponAnchor,
            new Rotation(
                new VectorRotationDefinition(
                    orientationVector
                )
            ),
            new WeaponSpriteOrientation(
                spriteRenderer,
                orientationVector
            )
        ) {}
    
    private WeaponOrientation(
        Rotatable weaponRotatable,
        Rotation rotation,
        Orientation spriteOrientation
    )
    {
        this.weaponRotatable = weaponRotatable;
        this.rotation = rotation;
        this.spriteOrientation = spriteOrientation;
    }
    
    
    public void Orient()
    {
        weaponRotatable.RotateAs(rotation);
        spriteOrientation.Orient();
    }
}