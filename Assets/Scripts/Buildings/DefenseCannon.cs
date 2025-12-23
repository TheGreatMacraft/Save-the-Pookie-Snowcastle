using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCannon : BuildingBase
{
    // Variables to be Assigned in Inspector
    [SerializeField] private float shootSpeed;
    [SerializeField] private float shootCooldown;
    
    [SerializeField] private HitEssentials projectileProperties;
    
    [SerializeField] private int maxTargetsAtOnce;
    [SerializeField] private int shootCost; 

    // External Objects Necessary
    public GameObject projectilePrefab;
    
    // Variables used in Script
    private readonly List<GameObject> enemiesInRange = new();
    private int targetsCurrentlyShooting;

    protected override void SetupComponents()
    {
        base.SetupComponents();
        
        if(projectileProperties == null)
            projectileProperties = GetComponent<HitEssentials>();
    }

    // Adding to the List of Enemies in Range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            enemiesInRange.Add(collision.gameObject);
    }

    // Removing from the List of Enemies in Range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemiesInRange.Contains(collision.gameObject))
            enemiesInRange.Remove(collision.gameObject);
    }

    private void Update()
    {
        if (isEnabled)
            CheckForEnemies();
    }
    
    private void CheckForEnemies()
    {
        // If Max Targets Reached, no need to Find More
        if (targetsCurrentlyShooting >= maxTargetsAtOnce)
            return;

        if (enemiesInRange.Count > 0)
            FindEnemy();
    }
    
    private void FindEnemy()
    {
        StartCoroutine(ShootTillDeadOrOutside(enemiesInRange[0]));
        targetsCurrentlyShooting++;
    }
    
    // Targeting the Enemies in Range
    private IEnumerator ShootTillDeadOrOutside(GameObject target)
    {
        while (target != null && enemiesInRange.Contains(target))
        {
            var shootDirection = GetAimDirection(transform.position, shootSpeed, target.transform.position,
                target.GetComponent<Rigidbody2D>().velocity);
            Shoot(shootDirection);
            yield return new WaitForSeconds(shootCooldown);
        }

        targetsCurrentlyShooting--;
    }

    //Shoot the Projectile
    private void Shoot(Vector2 shootDirection)
    {
        // Decrease the Currency
        HealthCastle.Instance.DecreaseHealth(shootCost);

        Quaternion shootRotation = Quaternion.FromToRotation(Vector2.right, shootDirection);
        
        // Spawn Projectile
        ShootingComponent.SpawnProjectile(
            projectilePrefab,
            shootRotation,
            transform.position,
            shootSpeed,
            projectileProperties);
    }

    //Perplexity - Calculating the Direction of the Snowball - Doesnt Work Well!!!
    public static Vector2 GetAimDirection(
        Vector2 shooterPos,
        float bulletSpeed,
        Vector2 enemyPos,
        Vector2 enemyVelocity)
    {
        // Direction to enemy
        Vector2 toEnemy = enemyPos - shooterPos;

        // If enemy is exactly on top of shooter
        if (toEnemy.sqrMagnitude < 0.0001f)
            return Vector2.right; // arbitrary fallback

        float distance = toEnemy.magnitude;

        // Clamp insane velocities (prevents NaN explosions)
        enemyVelocity = Vector2.ClampMagnitude(enemyVelocity, 50f);

        // Quadratic coefficients
        float a = enemyVelocity.sqrMagnitude - bulletSpeed * bulletSpeed;
        float b = 2f * Vector2.Dot(enemyVelocity, toEnemy);
        float c = toEnemy.sqrMagnitude;

        float discriminant = b * b - 4f * a * c;

        // If no real solution OR bullet too slow → fallback to direct aim
        if (discriminant < 0f || Mathf.Abs(a) < 0.0001f)
            return toEnemy.normalized;

        float sqrtDisc = Mathf.Sqrt(discriminant);

        // Two possible times
        float t1 = (-b - sqrtDisc) / (2f * a);
        float t2 = (-b + sqrtDisc) / (2f * a);

        // Pick the smallest positive time
        float t = Mathf.Min(t1, t2);
        if (t < 0f) t = Mathf.Max(t1, t2);

        // If still negative → fallback
        if (t < 0f)
            return toEnemy.normalized;

        // Predict enemy position
        Vector2 predictedPos = enemyPos + enemyVelocity * t;

        // Final direction
        Vector2 finalDir = predictedPos - shooterPos;

        // Avoid zero vectors
        if (finalDir.sqrMagnitude < 0.0001f)
            return toEnemy.normalized;

        return finalDir.normalized;
    }


    // Stop All Coroutines when Cannon Gets Disabled
    protected override void OnBuildingDisable()
    {
        base.OnBuildingDisable();
        
        StopAllCoroutines();
    }
}