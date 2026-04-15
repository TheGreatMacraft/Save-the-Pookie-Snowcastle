using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    [Header("Roll Settings")]
    public float rollSpeed = 12f;
    public float rollDuration = 0.35f;
    public float rollCooldown = 0.6f;
    public float rollDeceleration = 18f;

    [Header("State")]
    public bool isRolling = false;
    public bool canRoll = true;

    private Rigidbody2D rb;
    private Vector2 rollDirection;
    private float rollTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Trigger roll
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            StartRoll();
        }

        // Handle roll movement
        if (isRolling)
        {
            rollTimer += Time.deltaTime;

            // Smooth deceleration for juicy feel
            float t = rollTimer / rollDuration;
            float currentSpeed = Mathf.Lerp(rollSpeed, 0f, t);

            rb.velocity = rollDirection * currentSpeed;

            if (rollTimer >= rollDuration)
                EndRoll();
        }
    }

    void StartRoll()
    {
        canRoll = false;
        isRolling = true;
        rollTimer = 0f;

        // Roll in movement direction OR last facing direction
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rollDirection = input.sqrMagnitude > 0.1f ? input.normalized : transform.right;

        // Initial burst
        rb.velocity = rollDirection * rollSpeed;

        // Optional: temporary invulnerability
        // StartCoroutine(InvulnerabilityFrames());
        
        // Cooldown
        Invoke(nameof(ResetRoll), rollCooldown);
    }

    void EndRoll()
    {
        isRolling = false;
        rb.velocity = Vector2.zero;
    }

    void ResetRoll()
    {
        canRoll = true;
    }
}