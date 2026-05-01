public interface Magazine
{
    bool IsEmpty();
    void SpendAmmo(int amount);
    void Restore();
}


// Projectile

public interface Projectile : 
    Location,
    ProjectileInitializazion,
    ColliderListener,
    Terminable
{
    void Launch(float speed, float lifeTime);
}

public interface ProjectileInitializazion
{
    void Initialize(
        Collection<Projectile> projectileRegistry,
        string targetTag
    );
}