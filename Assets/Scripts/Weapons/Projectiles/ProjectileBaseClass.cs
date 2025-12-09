using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBaseClass : MonoBehaviour
{
    [System.NonSerialized]
    public float damage;
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
            collision.gameObject.GetComponent<EnemyHealth>().DecreaseHelth(damage);
            Destroy(gameObject);
        }
    }

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
