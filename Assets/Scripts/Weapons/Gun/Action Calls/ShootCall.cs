public sealed class ShootCall : ActionCall
{
    private readonly Magazine magazine;
    
    private readonly int ammoPerShot;
    private readonly float spreadAngle;
    
    private readonly ActionExecution reloadAction;
    
    private readonly Spawner projectileSpawner;
    private readonly Location projectileSpawnPoint;
    private readonly Rotation projectileSpawnRotation;

    private readonly CameraShake cameraShake;
    private readonly float shakeMagnitude;
    private readonly float shakeDuration;


    public ShootCall(
        Magazine magazine,
        int ammoPerShot,
        float spreadAngle,
        ActionExecution reloadAction,
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
        this.spreadAngle = spreadAngle;
        this.reloadAction = reloadAction;
        this.projectileSpawner = projectileSpawner;
        this.projectileSpawnPoint = projectileSpawnPoint;
        this.projectileSpawnRotation = projectileSpawnRotation;
        this.cameraShake = cameraShake;
        this.shakeMagnitude = shakeMagnitude;
        this.shakeDuration = shakeDuration;
    }
    
    
    public void Call()
    {
        if (magazine.IsEmpty())
        {
            reloadAction.Execute();
            return;
        }
        
        for (int i = 0; i < ammoPerShot; i++)
        {
            float angle = UnityEngine.Random.Range(-spreadAngle/2, spreadAngle/2);
            
            projectileSpawner.SpawnAt(
                projectileSpawnPoint,
                projectileSpawnRotation.AddDegrees(angle)
                );
        }
        
        cameraShake.Shake(shakeMagnitude,shakeDuration);
        magazine.SpendAmmo(ammoPerShot);
    }
}