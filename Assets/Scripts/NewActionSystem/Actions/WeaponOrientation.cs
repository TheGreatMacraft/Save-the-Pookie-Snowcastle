public sealed class WeaponOrientation : 
    Orientation
{
    private readonly Rotatable weaponRotatable;
    private readonly Rotation rotation;

    
    public WeaponOrientation(
        Rotatable weaponRotatable,
        Vector direction
        )
        : this(weaponRotatable,
            new Rotation(
                new VectorRotationDefinition(
                    direction
                    )
            )) {}

    private WeaponOrientation(
        Rotatable weaponRotatable,
        Rotation  rotation
    )
    {
        this.weaponRotatable = weaponRotatable;
        this.rotation = rotation;
    }
    
    
    public void Orient()
    {
        weaponRotatable.RotateAs(rotation);
    }
}