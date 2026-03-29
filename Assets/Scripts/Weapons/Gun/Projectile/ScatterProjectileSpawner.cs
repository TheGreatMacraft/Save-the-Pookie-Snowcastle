public class ScatterProjectileSpawner : 
    Spawner
{
    private readonly Spawner projectileSpawner;
    private readonly int numberOfProjectiles;


    public ScatterProjectileSpawner(
        Spawner projectileSpawner,
        int numberOfProjectiles)
    {
        this.projectileSpawner = projectileSpawner;
        this.numberOfProjectiles = numberOfProjectiles;
    }
    
    
    public void SpawnAt(Location spawnPoint, Rotation facingRotation)
    {
        float angle = 360f / numberOfProjectiles;
        
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            projectileSpawner.SpawnAt(
                spawnPoint,
                new Rotation(
                    new ConstantRotationDefinition(
                        facingRotation.Degrees() + angle * i
                    )
                )
            );
        }
    }
}