public sealed class ShootCall : ActionCall
{
    private readonly Magazine magazine;
    
    private readonly int ammoPerShot;
    private readonly Spawner projectileSpawner;
    
    private readonly Location projectileSpawnPoint;
    private readonly Rotation projectileSpawnRotation;


    public ShootCall(
        Magazine magazine,
        int ammoPerShot,
        Spawner projectileSpawner,
        Location projectileSpawnPoint,
        Rotation projectileSpawnRotation
        )
    {
        this.magazine = magazine;
        this.ammoPerShot = ammoPerShot;
        this.projectileSpawner = projectileSpawner;
        this.projectileSpawnPoint = projectileSpawnPoint;
        this.projectileSpawnRotation = projectileSpawnRotation;
    }
    
    
    public void Call()
    {
        if (magazine.IsEmpty()) return;

        for (int i = 0; i < ammoPerShot; i++)
        {
            projectileSpawner.SpawnAt(
                projectileSpawnPoint,
                projectileSpawnRotation
                );
        }
        
        magazine.SpendAmmo(ammoPerShot);
    }
}