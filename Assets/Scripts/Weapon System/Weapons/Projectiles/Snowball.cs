using UnityEngine;

public class Snowball : ProjectileBase
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
        for (var i = 0; i < subSnowballsCount; i++) SpawnSnowball(i, randomAngleShift);
    }

    private void SpawnSnowball(int index, float randomAngleShift)
    {
        var angle = index * (360f / subSnowballsCount) + randomAngleShift;
        var rotation = Quaternion.Euler(0f, 0f, angle);

        var newProjectile = Instantiate(subSnowballPrefab, transform.position, rotation);

        var projectileBaseClass = newProjectile.GetComponent<ProjectileBase>();
        projectileBaseClass.damageAmount = subSnowballDamage;

        var rb = newProjectile.GetComponent<Rigidbody2D>();
        Vector2 direction = rotation * Vector2.right;
        rb.velocity = direction.normalized * subSnowballSpeed;
    }
}