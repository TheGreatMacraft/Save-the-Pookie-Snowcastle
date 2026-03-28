using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]

public sealed class PhysicalMovementComponent : 
    MonoBehaviour,
    PhysicalMovement
{
    private Rigidbody2D movement;
    private float speedMultiplier = 10;

    private void Awake()
    {
        movement = new ComponentInObject<Rigidbody2D>(
            gameObject,
            null
            ).Value();
    }

    public void AddConstant(Vector direction, float speed)
    {
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
}