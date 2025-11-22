using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSnowballsPlayer : MonoBehaviour
{
    public static ShootingSnowballsPlayer Instance;

    public GameObject snowball;
    public float shootSpeed = 12f;

    public GameObject gunAnchor;
    public GameObject gunShootPoint;

    public KeyCode shootKey = KeyCode.E;

    private Vector3 shootDirection;
    private Vector3 mousePosition;
    private float shootAngle;

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        shootDirection = (mousePosition - gunAnchor.transform.position).normalized;

        shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        gunAnchor.transform.rotation = Quaternion.Euler(0f, 0f, shootAngle);

        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        HealthCastle.Instance.DecreaseHelth(1);

        GameObject newSnowball = Instantiate(snowball, gunShootPoint.transform.position, Quaternion.Euler(0f, 0f, shootAngle));
        Rigidbody2D rb = newSnowball.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * shootSpeed;
    }
}

