using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables to be Assigned in Inspector
    public float moveSpeed;
    
    // External Objects Necessary
    public Rigidbody2D playerRb;

    // Variables used in Script
    private Vector2 movementVector;

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
        if (playerRb == null)
            playerRb = GetComponent<Rigidbody2D>();
    }

    private Vector2 GetRawAxes()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MovePlayer()
    {
        playerRb.MovePosition(playerRb.position + movementVector * (moveSpeed * Time.fixedDeltaTime));
    }
}