using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSnowball : ProjectileBaseClass
{
    public int subSnowballsCount;
    public float subSnowballSpeed;
    public int subSnowballDamage;

    public GameObject subSnowballPrefab;

    public bool isDestroyedByAbility;

    public override void SelfDestroy()
    {
        base.SelfDestroy();

        if(!isDestroyedByAbility) { return; }

        float randomAngleShift = Random.Range(1f, 360f);
        for (int i = 0; i < subSnowballsCount; i++)
        {
            SpawnSnowball(i, randomAngleShift);
        }
    }

    private void SpawnSnowball(int index, float randomAngleShift)
    {
        float angle = index * (360f / subSnowballsCount) + randomAngleShift;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject newProjectile = Instantiate(subSnowballPrefab, transform.position, rotation);

        ProjectileBaseClass projectileBaseClass = newProjectile.GetComponent<ProjectileBaseClass>();
        projectileBaseClass.damage = subSnowballDamage;

        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        Vector2 direction = rotation * Vector2.right;
        rb.velocity = direction.normalized * subSnowballSpeed;
    }
}
