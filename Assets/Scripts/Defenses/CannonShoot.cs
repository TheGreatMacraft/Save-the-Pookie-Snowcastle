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
            Vector2 shootDirection = (target.transform.position - transform.position).normalized;
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
}
