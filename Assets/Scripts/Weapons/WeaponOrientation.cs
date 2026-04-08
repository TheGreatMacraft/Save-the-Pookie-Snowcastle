public sealed class WeaponOrientation : 
    Orientation
{
    private readonly Rotatable weaponRotatable;
    private readonly Rotation rotation;

    public WeaponOrientation(
        PhysicalBody weaponAnchor,
        Location targetLocation
        )
        : this(
            weaponAnchor,
            new Rotation(
                new VectorRotationDefinition(
                    new Vector(
                        new PointToPointVectorDefinition(
                            weaponAnchor,
                            targetLocation
                            )
                        )
                    )
                )
            ){}
    
    private WeaponOrientation(
        Rotatable weaponRotatable,
        Rotation rotation
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