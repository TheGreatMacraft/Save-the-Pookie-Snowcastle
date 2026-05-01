using UnityEngine;

public sealed class ScatterCannonComponent : 
    GunComponent
{
    [Header("Ability Components")]
    [SerializeField] private float abilityCooldown;
    
    [Header("Scattered Projectile Components")]
    [SerializeField] private float subProjectileSpeed;
    [SerializeField] private float subProjectileDamage;
    [SerializeField] private float subProjectileLifeTime;
    [SerializeField] private ProjectileComponent subProjectilePrefab;
    [SerializeField] private int numberOfProjectiles;
    

    protected override void Start()
    {
        base.Start();

        abilityAction = new ExecutionWithCooldown(
            new ConstantExecution(
                new ScatterCall(
                    new ScatterProjectileSpawner(
                        new StandardProjectileSpawner(
                            subProjectileSpeed,
                            subProjectileLifeTime,
                            new GameObjectBuilder<ProjectileComponent>(
                                subProjectilePrefab
                            ),
                            new NullCollection<Projectile>(),
                            targetTag
                        ),
                        numberOfProjectiles
                    ),
                    firedProjectiles
                )
            ),
            abilityCooldown,
            coroutineClock,
            false
        );
    }
}