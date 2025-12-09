using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotateToFaceCursor : MonoBehaviour
{
    public Transform gun;

    private void Start()
    {
        // Get Gun Transform from Child Object with Weapon Script
        gun = GetComponentInChildren<WeaponBaseClass>().transform;
    }

    public virtual void Update()
    {
        // INSERT: Flip Player Sprite to Face Mouse Cursor

        RotateGunToFaceCursor();
    }

    private void RotateGunToFaceCursor()
    {
        gun.transform.rotation = RotationToPointCursor(gun);
    }

    private Quaternion RotationToPointCursor(Transform gun)
    {
        // Get Mouse World Position
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        // Calculate Vector From Gun To Mouse Cursor
        Vector2 gunFacingDirection = (mouseWorldPosition - gun.position).normalized;

        // Return Rotation From Gun To Mouse Cursor
        float gunFacingAngle = Mathf.Atan2(gunFacingDirection.y, gunFacingDirection.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, gunFacingAngle);
    }
}
