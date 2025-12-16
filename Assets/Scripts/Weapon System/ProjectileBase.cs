using System;
using UnityEngine;

public abstract class ProjectileBase : MunitionBase
{
    // Gun That Fired Projectile
    public GunBase ownerGun;

    private void Update()
    {
        if (OutOfBounds())
            Destroy(gameObject);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            // If Enemy is Hit, Call Hit Target and Destroy this Projectile 
            WeaponComponent.HitTarget(
                collision.gameObject,
                damageAmount,
                transform.position, 
                knockbackStrength,
                knockbackDuration,
                ownerGun,
                targetTag
            );
            
            Destroy(gameObject);
        }
    }

    // Magic that checks if Game Object is Out of Map Bounds
    private bool OutOfBounds()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        var (mapWidth, mapHeight) = MapSize.instance.GetMapDimensions();

        return x < -mapWidth / 2 || x > mapWidth / 2 || y < -mapHeight / 2 || y > mapHeight / 2;
    }
}