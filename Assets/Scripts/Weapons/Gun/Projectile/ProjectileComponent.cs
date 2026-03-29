using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[RequireComponent(typeof(ColliderSensorComponent))]
[DisallowMultipleComponent]

public sealed class ProjectileComponent : 
    MonoBehaviour, 
    Projectile
{
    private PhysicalBody body;
    private PhysicalMovement movement;
    private ColliderSensor sensor;
    
    private Collection<Projectile> firedProjectiles;
    private string targetTag;

    private Impact allImpacts;
    

    private void Awake()
    {
        body = new ComponentInObject<PhysicalBody>(
            gameObject,
            null
        ).Value();
        
        movement = new ComponentInObject<PhysicalMovement>(
            gameObject,
            null
            ).Value();

        sensor = new ComponentInObject<ColliderSensor>(
            gameObject,
            new NullColliderSensor()
        ).Value();
        
        sensor.Connect(this);
    }

    
    public void Initialize(
        Collection<Projectile> projectileRegistry,
        string targetTag
        )
    {
        this.firedProjectiles = projectileRegistry;
        this.targetTag = targetTag;
        
        allImpacts = new ActionImpacts(
            new AllComponentsInObject<Impact>(
                gameObject,
                new NullImpact()).Value()
        );
    }

    public Vector3 Coordinates()
        => body.Coordinates();

    public void Launch(float speed)
    {
        firedProjectiles.Register(this);
        
        movement.AddConstant(
            new Vector(body),
            speed
            );
    }
    
    
    public void Terminate()
    {
        firedProjectiles.Unregister(this);
        sensor.Disconnect(this);
        Destroy(gameObject);
    }

    public void OnEnter(GameObject hitObject)
    {
        new WeaponPayload(
            targetTag,
            allImpacts,
            this
            ).Deliver(hitObject);
    }
}