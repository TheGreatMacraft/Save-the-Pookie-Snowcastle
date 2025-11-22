using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    public HurtEnemy hurtScript;

    public float moveSpeed = 3f;
    public bool nearbyCastle
    {
        get { return hurtScript.nearbyCastle; }
        set { hurtScript.nearbyCastle = value; }
    }

    public Rigidbody2D rb;

    private Vector2 movementVector;

    private void Start()
    {
        if(rb==null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!nearbyCastle)
            movementVector = (GameObject.Find("Castle").GetComponent<Transform>().position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if(!nearbyCastle)
            rb.MovePosition(rb.position + movementVector * moveSpeed * Time.fixedDeltaTime);
    }
}
