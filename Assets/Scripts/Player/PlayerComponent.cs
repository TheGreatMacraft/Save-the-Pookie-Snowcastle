using UnityEngine;
[RequireComponent(typeof(PhysicalMovement))]
[DisallowMultipleComponent]

public sealed class PlayerComponent : 
    MonoBehaviour
{
    [SerializeField] private float speed;
    private Movement legs;
    
    
    private void Awake()
    {
        legs = new Legs(
            new ComponentInObject<Force>(
                gameObject,
                new NullForce()
            ).Value(),
            new Vector(
                new InputAxisVectorDefinition()
            ),
            speed
        );
    }

    
    private void FixedUpdate()
    {
        legs.Move();
    }
}