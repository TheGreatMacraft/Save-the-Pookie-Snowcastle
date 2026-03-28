public sealed class StandardProjectileSpawner : 
    Spawner
{
    private readonly float speed;
    private readonly ObjectBuilder<ProjectileComponent> unityProjectileBuilder;
    private readonly Collection<Projectile> firedProjectiles;
    private readonly string targetTag;
    
    
    public StandardProjectileSpawner(
        float speed,
        ObjectBuilder<ProjectileComponent> unityProjectileBuilder,
        Collection<Projectile> firedProjectiles,
        string targetTag
        )
    {
        this.speed = speed;
        this.unityProjectileBuilder = unityProjectileBuilder;
        this.firedProjectiles = firedProjectiles;
        this.targetTag = targetTag;
    }
        
    
    public void SpawnAt(Location spawnPoint, Rotation facingRotation)
    {
        Projectile newProjectile = unityProjectileBuilder.Build(
            spawnPoint, 
            facingRotation
            );
        
        newProjectile.Initialize(firedProjectiles, targetTag);
        newProjectile.Launch(speed);
    }
}