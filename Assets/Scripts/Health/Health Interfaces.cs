public interface Damageable
{
    void TakeDamage(float damage);
}

public interface Healable
{
    void Heal(float healAmount);
}

public interface Health : Damageable, Healable {}

public interface MortalityMonitor
{
    void Report(float currentHealth);
}

public interface Mortal
{
    void Die();
}

public interface HealthReporter
{
    void Report(Media media, MortalityMonitor monitor);
}