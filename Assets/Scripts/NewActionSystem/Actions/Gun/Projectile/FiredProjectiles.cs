using System.Collections.Generic;

public sealed class FiredProjectiles : Collection<Projectile>
{
    List<Projectile> projectiles = new();

    
    public void Register(Projectile newProjectile)
    {
        projectiles.Add(newProjectile);
    }

    public void Unregister(Projectile newProjectile)
    {
        if (projectiles.Contains(newProjectile))
            projectiles.Remove(newProjectile);
    }
    
    public IEnumerable<Projectile> AllElements()
        => projectiles;
}