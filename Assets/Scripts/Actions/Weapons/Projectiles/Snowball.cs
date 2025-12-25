using UnityEngine;

public class Snowball : ProjectileEssentials
{
    public int subSnowballsCount;
    public float subSnowballSpeed;
    public int subSnowballDamage;

    public GameObject subSnowballPrefab;

    public bool isDestroyedByAbility;

    public override void SelfDestroy()
    {
        base.SelfDestroy();

        if (!isDestroyedByAbility) return;

        var randomAngleShift = Random.Range(1f, 360f);
        
        for (var i = 0; i < subSnowballsCount; i++)
        {
            var angle = i * (360f / subSnowballsCount) + randomAngleShift;
            var rotation = Quaternion.Euler(0f, 0f, angle);
            
            ShootingComponent.SpawnProjectile(
                subSnowballPrefab,
                rotation,
                transform.position,
                subSnowballSpeed,
                this);
        }
    }
}