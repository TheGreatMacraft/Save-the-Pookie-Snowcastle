using UnityEngine;

public sealed class ScatterCannonComponent : 
    GunComponent
{
    [Header("Ability Components")]
    [SerializeField] private float abilityCooldown;
    
    [Header("Scattered Projectile Components")]
    [SerializeField] private float subProjectileSpeed;
    [SerializeField] private float subProjectileDamage;
    [SerializeField] private ProjectileComponent subProjectilePrefab;
    [SerializeField] private int numberOfProjectiles;
    
    private readonly Spawner scatteredProjectiles;

    protected override ActionExecution AddAbility()
    {
        return new InstantAction(
            new ScatterCall(
                new ScatterProjectileSpawner(
                    new StandardProjectileSpawner(
                        subProjectileSpeed,
                        new GameObjectBuilder<ProjectileComponent>(
                            subProjectilePrefab
                            ),
                        new NullCollection<Projectile>(),
                        targetTag
                        ),
                    numberOfProjectiles
                    ),
                firedProjectiles
                ),
            abilityCooldown,
            coroutineClock
        );
    }
}