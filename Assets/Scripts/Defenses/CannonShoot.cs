using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonShoot : MonoBehaviour
{
    public DefenseBasics defenseBasicsScript;

    public GameObject snowball;
    public float shootSpeed = 12f;
    public float shootCooldown = 1.25f;
    public int maxTargetsAtOnce = 1;
    private int targetsCurrentlyShooting;

    private List<GameObject> enemiesInRange = new List<GameObject>();

    //Shoot the Snowball
    private void Shoot(Vector2 shootDirection)
    {
        HealthCastle.Instance.DecreaseHelth(2); 

        GameObject newSnowball = Instantiate(snowball, transform.position, Quaternion.Euler(0f, 0f, 0f));
        Rigidbody2D rb = newSnowball.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * shootSpeed;
    }

    // Targeting the Enemies in Range
    private IEnumerator ShootTillDead(GameObject target)
    {
        while (target != null && !defenseBasicsScript.isDisabled)
        {
            Vector2 shootDirection = GetAimDirection(transform.position,shootSpeed,target.transform.position,target.GetComponent<Rigidbody2D>().velocity);
            Shoot(shootDirection);
            yield return new WaitForSeconds(shootCooldown);
        }
        targetsCurrentlyShooting--;
    }
    private void FindEnemy()
    {
        StartCoroutine(ShootTillDead(enemiesInRange[0]));
        enemiesInRange.RemoveAt(0);
        targetsCurrentlyShooting++;
    }
    private void CheckForEnemies()
    {
        if (targetsCurrentlyShooting >= maxTargetsAtOnce)
            return;

        if (enemiesInRange.Count > 0)
            FindEnemy();
    }

    // Adding and Removing to the List of Enemies in Range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
            enemiesInRange.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemiesInRange.Contains(collision.gameObject))
            enemiesInRange.Remove(collision.gameObject);
    }

    private void Awake()
    {
        snowball = PrefabManager.instance.snowball;
    }

    private void Start()
    {
        DefenseTracker.instance.Register(gameObject);
    }

    private void Update()
    {
        if (!defenseBasicsScript.isDisabled)
            CheckForEnemies();
    }

    //Perplexity - Calculating the Direction of the Snowball - Doesnt Work Well!!!
    public static Vector2 GetAimDirection(Vector2 shooterPos, float bulletSpeed,
                                        Vector2 enemyPos, Vector2 enemyVelocity)
    {
        Vector2 targetDir = enemyPos - shooterPos;  // Direction to current enemy position
        float distance = targetDir.magnitude;

        // Quadratic equation: a*t^2 + b*t + c = 0
        float a = enemyVelocity.sqrMagnitude - bulletSpeed * bulletSpeed;
        float b = 2 * Vector2.Dot(enemyVelocity, targetDir);
        float c = targetDir.sqrMagnitude;

        float discriminant = b * b - 4 * a * c;

        if (discriminant < 0 || a == 0)
        {
            // No real solution or linear case (fire directly at current position)
            return targetDir.normalized;
        }

        // Use the smallest positive root for closest intercept
        float t = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

        if (t < 0) t = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        if (t < 0) return Vector2.zero;  // Cannot intercept

        // Predicted enemy position at intercept time
        Vector2 predictedPos = enemyPos + enemyVelocity * t;
        return (predictedPos - shooterPos).normalized;
    }
}
