using System;
using UnityEngine;

public class Skis : ToolBase
{
    /*
    
    // Assigned in Inspector
    public float baseSpeedMultiplier = 1.5f;
    
    // Used in Script
    public Rigidbody2D rb;
    public PlayerMovement movementScript;

    private float baseSpeed;
    
    private bool _toolActive = false;
    private bool toolActive
    {
        get => _toolActive;
        set
        {
            _toolActive = value;
            movementScript.isSliding = value;
        }
    }

    protected override void SetupComponents()
    {
        if (rb == null)
            rb = GetComponentInParent<Rigidbody2D>();

        movementScript = GetComponentInParent<PlayerMovement>();

        baseSpeed = movementScript.moveSpeed;

        /*
        // Prevent stacking
        actionModules["Tool"].cancelCallOverride = () =>
        {
            return toolActive; 
        };
        
        // 
        actionModules["Tool"].cancelCallAftermath = () =>
        {
            StopTool();
        };
        *//*
    }

    protected override void Tool()
    {
        Debug.Log("Skis");
        // Boost Speed
       //movementScript.moveSpeed = baseSpeed * baseSpeedMultiplier;
        
        toolActive = true;
    }

    private void StopTool()
    {
        Debug.Log("Cancel Tool");
        toolActive = false;
        //movementScript.moveSpeed = baseSpeed;
    }
    */
}
