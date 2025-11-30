using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingSnowballsPlayer : MonoBehaviour
{
    public GameObject snowball;
    public float shootSpeed = 12f;

    public GameObject gunAnchor;
    public GameObject gunShootPoint;

    private Vector3 shootDirection;
    private Vector3 mousePosition;
    private Vector2 stickInput;
    private float shootAngle;

    private void SetAngleKeyboardMouse()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0;

        shootDirection = (mousePosition - gunAnchor.transform.position).normalized;

        shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        gunAnchor.transform.rotation = Quaternion.Euler(0f, 0f, shootAngle);
    }

    private void SetAngleController()
    {
        stickInput = Gamepad.current.leftStick.ReadValue();

        if (stickInput.sqrMagnitude > 0.1f)
        {
            shootDirection = (gunShootPoint.transform.position - gunAnchor.transform.position).normalized;

            shootAngle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;
            gunAnchor.transform.rotation = Quaternion.Euler(0f, 0f, shootAngle);
        }
    }

    private void Update()
    {
        if (Keyboard.current != null && Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero)
        {
            SetAngleKeyboardMouse();
        }

        if(Gamepad.current != null)
        {
            SetAngleController();
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        HealthCastle.Instance.DecreaseHelth(1);

        GameObject newSnowball = Instantiate(snowball, gunShootPoint.transform.position, Quaternion.Euler(0f, 0f, shootAngle));
        Rigidbody2D rb = newSnowball.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * shootSpeed;
    }
}

