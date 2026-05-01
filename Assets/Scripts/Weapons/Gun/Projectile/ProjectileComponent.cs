using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[RequireComponent(typeof(ColliderSensorComponent))]
[DisallowMultipleComponent]

public sealed class ProjectileComponent : 
    MonoBehaviour, 
    Projectile
{
    private Clock coroutineClock;
    
    private PhysicalBody body;
    private PhysicalMovement movement;
    private ColliderSensor sensor;
    
    private Collection<Projectile> firedProjectiles;
    private string targetTag;

    private Impact allImpacts;
    

    private void Awake()
    {
        coroutineClock = new CoroutineClock(this);
        
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
        
        allImpacts = new ActionImpacts(
            new AllComponentsInObject<Impact>(
                gameObject,
                new NullImpact()).Value()
        );
    }

    
    public void Initialize(
        Collection<Projectile> projectileRegistry,
        string targetTag
        )
    {
        this.firedProjectiles = projectileRegistry;
        this.targetTag = targetTag;
        
    }

    public Vector3 Coordinates()
        => body.Coordinates();

    public void Launch(float speed, float lifeTime)
    {
        firedProjectiles.Register(this);
        
        movement.AddConstant(
            new Vector(body),
            speed
            );
        
        coroutineClock.Schedule(
            Terminate,
            lifeTime
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