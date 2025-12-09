using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunBaseClass : MonoBehaviour
{
    // Number of Projectiles
    public int projectilesBeforeReload;
    public int projectilesPerShoot;

    // Time
    public float reloadTime;
    public float timeBetweenShots;

    // Bulet
    public int projectileDamage;
    public float projectileSpeed;

    // Spread
    public float spread;

    // Camera Shake
    public float cameraShakeMagnitude;
    public float cameraShakeDuration;

    // Values To Be Assigned in Inspector
    public GameObject projectilePrefab;
    public GameObject gunRotationAnchor;
    public GameObject projectileSpawnPoint;

    // Input Action Variables
    [SerializeField] private PlayerInput playerInput;

    // Values Used in Script
    Vector2 shootDirection;

    int currentProjectileCount;
    bool isReloading;
    bool canShoot = true;

    // Projectile Tracker
    [System.NonSerialized]
    public List<GameObject> projectilesShot = new List<GameObject>();


    private void Start()
    {
        // Set Ammo in Magazine to Max Allowed
        currentProjectileCount = projectilesBeforeReload;

        SetButtonFunctions();
    }
    void Update()
    {
        if (KeyboardAndMouseConnected())
        {
            gunRotationAnchor.transform.rotation = AngleToPointCursor(gunRotationAnchor);
        }
    }

    public void ShootCall(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        if (currentProjectileCount == 0) { return; }

        if(!canShoot) {  return; }

        // Spawn Projectiles
        for(int i=0; i < projectilesPerShoot; i++)
            SpawnProjectile();

        // Reduce Ammunition in Magazine
        currentProjectileCount -= projectilesPerShoot;

        // Start Cooldown
        canShoot = false;
        StartCoroutine(AllowShootingAfterCooldown());

        // Camera Shake
        StartCoroutine(CameraShake.instance.ShakeCamera(cameraShakeMagnitude, cameraShakeDuration));
    }

    public void SpawnProjectile()
    {
        float spreadAngle = Random.Range(-spread, spread);

        Quaternion spreadRotation = gunRotationAnchor.transform.rotation * Quaternion.Euler(0f, 0f, spreadAngle);
        Quaternion facingRotation = spreadRotation * Quaternion.Euler(0f, 0f, -90f);

        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, facingRotation);
        
        ProjectileBaseClass projectileBaseClass = newProjectile.GetComponent<ProjectileBaseClass>();
        projectileBaseClass.damage = projectileDamage;
        projectileBaseClass.ownerGun = this;

        Vector2 spreadDirection = spreadRotation * Vector2.right;
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.velocity = spreadDirection * projectileSpeed;

        projectilesShot.Add(newProjectile);
    }

    public IEnumerator AllowShootingAfterCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }    

    public void ReloadCall(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        if (currentProjectileCount == projectilesBeforeReload) { return; }

        if(isReloading) { return; }

        // Call Reload after Reload Time
        isReloading = true;
        StartCoroutine(ReloadAfterTime());
    }

    public IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(reloadTime);

        // Set current ammo to max ammo
        currentProjectileCount = projectilesBeforeReload;
        isReloading = false;
    }

    public void AbilityCall(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }

        Ability();
    }

    public virtual void Ability() { }

    private Quaternion AngleToPointCursor(GameObject gunRotationAnchor)
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        shootDirection = (mouseWorldPosition - gunRotationAnchor.transform.position).normalized;

        float shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, shootAngle);
    }

    bool KeyboardAndMouseConnected()
    {
        return Keyboard.current != null && Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero;
    }

    bool ControllerConnected()
    {
        return Gamepad.current != null;
    }

    void SetButtonFunctions()
    {
        var shootAction = playerInput.actions["ShootButton"];
        var reloadAction = playerInput.actions["ReloadButton"];
        var abilityAction = playerInput.actions["AbilityButton"];

        shootAction.performed += ShootCall;
        shootAction.Enable();

        reloadAction.performed += ReloadCall;
        reloadAction.Enable();

        abilityAction.performed += AbilityCall;
        abilityAction.Enable();
    }
}