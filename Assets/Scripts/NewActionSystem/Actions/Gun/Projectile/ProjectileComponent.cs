using UnityEngine;
[RequireComponent(typeof(PhysicalBodyComponent))]
[RequireComponent(typeof(PhysicalMovementComponent))]
[DisallowMultipleComponent]

public sealed class ProjectileComponent : 
    MonoBehaviour, 
    Projectile
{
    private PhysicalBody body;
    private PhysicalMovement movement;

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
    }

    private Collection<Projectile> firedProjectiles;
    private string targetTag;

    private Impact allImpacts;

    
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
        Destroy(gameObject);
    }
    
    
    // Hit Target on Collision using Unity's Collision Detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        new FilteredTarget(
            new ComponentInObject<Target>(
                other, 
                new NullTarget()
                ).Value(),
            targetTag
        ).Value()
            .Hit(allImpacts, this);
    }
}