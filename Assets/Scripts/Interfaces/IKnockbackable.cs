using UnityEngine;

public interface IKnockbackable
{
    void ApplyKnockback(float force, Vector2 knockbackDirection);
}