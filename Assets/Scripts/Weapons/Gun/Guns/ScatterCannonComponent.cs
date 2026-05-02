using System.Collections.Generic;
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


    private readonly List<ActionExecution> abilityAction = new(1);


    public override ActionExecution Ability()
    {
        if (abilityAction.Count == 0)
        {
            abilityAction.Add(
                new ExecutionWithCooldown(
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
                    CoroutineClock(),
                    false
                )
            );
        }
        
        return abilityAction[0];
    }
}