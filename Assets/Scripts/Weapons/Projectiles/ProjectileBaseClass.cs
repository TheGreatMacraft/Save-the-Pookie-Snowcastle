using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBaseClass : MonoBehaviour
{
    [System.NonSerialized]
    public int damage;
    public float knockbackStrength;

    // Gun That Fired Projectile
    public GunBaseClass ownerGun;

    private void Update()
    {
        if (OutOfBounds())
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            // If Enemy is Hit, Decrease it's Hit Points and Destroy this Projectile 
            WeaponRelatedUtils.HitEnemy(collision.gameObject, ownerGun, damage, transform.position,knockbackStrength);
            Destroy(gameObject);
        }
    }

    // Magic that checks if Game Object is Out of Map Bounds
    private bool OutOfBounds()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        var (mapWidth, mapHeight) = MapSize.instance.GetMapDimensions();

        return x < -mapWidth / 2 || x > mapWidth / 2 || y < -mapHeight / 2 || y > mapHeight / 2;
    }

    private void OnDestroy()
    {
        SelfDestroy();
    }

    public virtual void SelfDestroy()
    {
        // Remove Projectile from Owner Gun List
        if (ownerGun != null && ownerGun.projectilesShot.Contains(gameObject))
            ownerGun.projectilesShot.Remove(gameObject);
    }
}
