using UnityEngine;

public class PlayerMovement : MovementBase
{
    [Header("Ski Movement")]
    public float pushForce = 5f;
    public float pushInterval = 0.25f;
    public float maxSpeed = 1f;         

    private float pushTimer;
    
    [Header("Ski Steering")]
    public float steeringStrength = 2.5f;   // How much input can turn velocity
    public float lateralDamping = 0.9f;      // How much sideways sliding is reduced (0–1)
    public float minSteerSpeed = 0.2f;       // No steering if nearly stopped
    
    // Used in Script
    public Vector2 movementVector;
    public bool isSliding;

    private void Start()
    {
        // Register Player to The Tracker
        PlayerTracker.instance.Register(gameObject);
        
        SetupComponents();
    }

    private void Update()
    {
        movementVector = GetRawAxes();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void SetupComponents()
    {
        // Rigidbody
        if (entityRb == null)
            entityRb = GetComponent<Rigidbody2D>();
        
        entityRb.drag = 0.2f;        // Low drag = sliding
        entityRb.angularDrag = 0.05f;

    }

    private Vector2 GetRawAxes()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MovePlayer()
    {
        if(isSliding)
            SkiMovement();
        else
            entityRb.velocity = movementVector * moveSpeed;
    }

    private void SkiMovement()
    {
        pushTimer -= Time.fixedDeltaTime;

        // === Ski push ===
        if (movementVector.sqrMagnitude > 0.01f && pushTimer <= 0f)
        {
            Vector2 pushDir = movementVector.normalized;
            entityRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
            pushTimer = pushInterval;
        }

        // === Steering logic ===
        Vector2 velocity = entityRb.velocity;
        float speed = velocity.magnitude;

        if (speed > minSteerSpeed && movementVector.sqrMagnitude > 0.01f)
        {
            Vector2 desiredDir = movementVector.normalized;

            // Gradually rotate velocity toward input direction
            Vector2 steeredVelocity = Vector2.Lerp(
                velocity.normalized,
                desiredDir,
                steeringStrength * Time.fixedDeltaTime
            ).normalized * speed;

            entityRb.velocity = steeredVelocity;
        }

        // === Side-slip reduction (carving effect) ===
        if (speed > minSteerSpeed)
        {
            Vector2 forward = entityRb.velocity.normalized;
            Vector2 sideways = Vector2.Perpendicular(forward);

            float sidewaysSpeed = Vector2.Dot(entityRb.velocity, sideways);
            entityRb.velocity -= sideways * sidewaysSpeed * (1f - lateralDamping);
        }

        // === Speed clamp ===
        if (entityRb.velocity.magnitude > maxSpeed)
        {
            entityRb.velocity = entityRb.velocity.normalized * maxSpeed;
        }
    }

}