using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[DisallowMultipleComponent]

public sealed class WeaponOrientationComponent : 
    MonoBehaviour
{
    [SerializeField] private Camera camera;
    
    private Rotatable weaponRotatable;
    private Rotation rotation;

    private void Awake()
    {
        weaponRotatable = new ComponentInObject<Rotatable>(
            gameObject,
            new NullRotatable()
            ).Value();
        
        Location weaponHandle = new ComponentInObject<Location>(
            gameObject,
            new NullLocation()
        ).Value();

        rotation = new Rotation(
            new VectorRotationDefinition(
                new Vector(
                    new PointToPointVectorDefinition(
                        weaponHandle,
                        new MouseCursorSCREENPosition()
                    )
                )
            )
        );
    }
    
    
    public void Update()
    {
        weaponRotatable.RotateAs(rotation);
    }
}