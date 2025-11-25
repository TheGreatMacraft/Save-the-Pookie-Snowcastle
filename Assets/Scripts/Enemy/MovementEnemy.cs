using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    public HurtEnemy hurtScript;

    public float moveSpeed = 3f;

    public Rigidbody2D rb;

    private Vector2 movementVector;

    private void Start()
    {
        if(rb==null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!hurtScript.nearbyCastle)
            movementVector = (GameObject.Find("Castle").GetComponent<Transform>().position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if(!hurtScript.nearbyCastle)
            rb.MovePosition(rb.position + movementVector * moveSpeed * Time.fixedDeltaTime);
    }
}
