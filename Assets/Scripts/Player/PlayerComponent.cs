using UnityEngine;
[RequireComponent(typeof(PhysicalMovement))]
[DisallowMultipleComponent]

public sealed class PlayerComponent : 
    MonoBehaviour,
    TargetLocationSource
{
    [SerializeField] private float speed;
    [SerializeField] private Camera camera;
    
    private Movement legs;
    private Location mouseWorldLocation;
    private Target mouseWorld;
    
    
    private void Awake()
    {
        mouseWorldLocation = new MouseCursorWORLDPosition(
            camera
        );
        
        legs = new Legs(
            new ComponentInObject<Force>(
                gameObject,
                new NullForce()
            ).Value(),
            new Vector(
                new InputAxisVectorDefinition()
            ),
            speed
        );
    }
    
    private void FixedUpdate()
    {
        legs.Move();
    }
    
    
    public Vector3 Coordinates()
        => mouseWorldLocation.Coordinates();
}