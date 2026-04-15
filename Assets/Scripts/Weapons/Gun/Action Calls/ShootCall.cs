public sealed class ShootCall : ActionCall
{
    private readonly Magazine magazine;
    
    private readonly int ammoPerShot;
    private readonly Spawner projectileSpawner;
    
    private readonly Location projectileSpawnPoint;
    private readonly Rotation projectileSpawnRotation;

    private readonly CameraShake cameraShake;
    private readonly float shakeMagnitude;
    private readonly float shakeDuration;


    public ShootCall(
        Magazine magazine,
        int ammoPerShot,
        Spawner projectileSpawner,
        Location projectileSpawnPoint,
        Rotation projectileSpawnRotation,
        CameraShake cameraShake,
        float shakeMagnitude,
        float shakeDuration
        )
    {
        this.magazine = magazine;
        this.ammoPerShot = ammoPerShot;
        this.projectileSpawner = projectileSpawner;
        this.projectileSpawnPoint = projectileSpawnPoint;
        this.projectileSpawnRotation = projectileSpawnRotation;
        this.cameraShake = cameraShake;
        this.shakeMagnitude = shakeMagnitude;
        this.shakeDuration = shakeDuration;
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
        
        cameraShake.Shake(shakeMagnitude,shakeDuration);
        magazine.SpendAmmo(ammoPerShot);
    }
}