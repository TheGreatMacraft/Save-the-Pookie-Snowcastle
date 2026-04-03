using System.Collections.Generic;
using UnityEngine;

public sealed class ScatterCall : 
    ActionCall
{
    private readonly Spawner scatteredProjectilesSpawner;
    private readonly Collection<Projectile> firedProjectiles;
    
    private readonly Rotation nullRotation 
        = new Rotation(new NullRotationDefinition());


    public ScatterCall(
        Spawner scatteredProjectilesSpawner,
        Collection<Projectile> firedProjectiles)
    {
        this.scatteredProjectilesSpawner = scatteredProjectilesSpawner;
        this.firedProjectiles = firedProjectiles;
    }
    

    public void Call()
    {
        List<Projectile> projectiles = new (
            firedProjectiles.AllElements()
            );
        
        foreach (
            Projectile projectile 
            in projectiles
            )
        {
            scatteredProjectilesSpawner.SpawnAt(
                projectile, 
                nullRotation
                );
            
            projectile.Terminate();
        }
    }
}