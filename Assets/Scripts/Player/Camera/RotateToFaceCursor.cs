using UnityEngine;
using UnityEngine.InputSystem;

public class RotateToFaceCursor : MonoBehaviour
{
    public Transform weapon;
    public Camera playerCamera;

    private void Start()
    {
        // Get Weapon Transform from Child Object with Weapon Script
        weapon = GetComponentInChildren<WeaponBase>().transform;
        
        // Get Camera from Player
        playerCamera = GetComponentInChildren<Camera>();
    }

    public virtual void Update()
    {
        // INSERT: Flip Player Sprite to Face Mouse Cursor

        RotateWeaponToFaceCursor();
    }

    private void RotateWeaponToFaceCursor()
    {
        weapon.transform.rotation = RotationToPointCursor();
    }

    private Quaternion RotationToPointCursor()
    {
        // Get Mouse World Position
        var mouseScreenPos = Mouse.current.position.ReadValue();
        var mouseWorldPosition = playerCamera.ScreenToWorldPoint(mouseScreenPos);

        return ShootingComponent.GetAimRotation(weapon.position, mouseWorldPosition);
    }
}