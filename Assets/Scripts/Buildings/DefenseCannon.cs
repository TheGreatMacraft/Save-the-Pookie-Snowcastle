using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCannon : DefenseBase
{
    // Variables to be Assigned in Inspector
    [SerializeField] private float shootSpeed;
    [SerializeField] private float shootCooldown;
    [SerializeField] private int damageAmount;
    [SerializeField] private float knockbackStrength;
    
    [SerializeField] private int maxTargetsAtOnce;
    [SerializeField] private int shootCost; 

    // External Objects Necessary
    public GameObject projectilePrefab;
    public GameObject gunRotationAnchor;
    public GameObject shootPoint;
    
    // Variables used in Script
    private readonly List<GameObject> enemiesInRange = new();
    private int targetsCurrentlyShooting;
    
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
        StartCoroutine(ShootTillDead(enemiesInRange[0]));
        enemiesInRange.RemoveAt(0);
        targetsCurrentlyShooting++;
    }
    
    // Targeting the Enemies in Range
    private IEnumerator ShootTillDead(GameObject target)
    {
        while (target != null)
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

        // Spawn Projectile
        ShootingComponent.SpawnProjectile(
            projectilePrefab,
            gunRotationAnchor,
            shootPoint,
            shootSpeed
            );
    }

    //Perplexity - Calculating the Direction of the Snowball - Doesnt Work Well!!!
    public static Vector2 GetAimDirection(Vector2 shooterPos, float bulletSpeed,
        Vector2 enemyPos, Vector2 enemyVelocity)
    {
        var targetDir = enemyPos - shooterPos; // Direction to current enemy position
        var distance = targetDir.magnitude;

        // Quadratic equation: a*t^2 + b*t + c = 0
        var a = enemyVelocity.sqrMagnitude - bulletSpeed * bulletSpeed;
        var b = 2 * Vector2.Dot(enemyVelocity, targetDir);
        var c = targetDir.sqrMagnitude;

        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0 || a == 0)
            // No real solution or linear case (fire directly at current position)
            return targetDir.normalized;

        // Use the smallest positive root for closest intercept
        var t = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

        if (t < 0) t = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        if (t < 0) return Vector2.zero; // Cannot intercept

        // Predicted enemy position at intercept time
        var predictedPos = enemyPos + enemyVelocity * t;
        return (predictedPos - shooterPos).normalized;
    }

    // Stop All Coroutines when Cannon Gets Disabled
    protected override void OnBuildingDisable()
    {
        StopAllCoroutines();
    }
}