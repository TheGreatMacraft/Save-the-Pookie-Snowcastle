using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]

public sealed class PhysicalMovementComponent : 
    MonoBehaviour,
    PhysicalMovement
{
    private Rigidbody2D movement;
    private readonly float speedMultiplier = 10;
    private bool isStunned = false;
    private Clock coroutineClock;

    private void Awake()
    {
        movement = new ComponentInObject<Rigidbody2D>(
            gameObject,
            null
            ).Value();

        coroutineClock = new CoroutineClock(this);
    }

    public void AddConstant(Vector direction, float speed)
    {
        if (isStunned) return;
        
        movement.AddForce(
            direction.Direction() * (speed * speedMultiplier)
            , ForceMode2D.Force);
    }

    public void AddImpulse(Vector direction, float amount)
    {
        movement.AddForce(
            direction.Direction() * (amount * speedMultiplier)
            , ForceMode2D.Impulse);
    }

    public void SetForce(Vector direction, float amount)
    {
        movement.velocity = direction.Direction() * amount;
    }

    public void ResetForce()
    {
        movement.velocity = Vector2.zero;
    }

    public void Stun(float duration)
    {
        isStunned = true;
        ResetForce();
        
        coroutineClock.Schedule(() =>
            {
                isStunned = false;
            },
            duration
            );
    }
}