using System;
using UnityEngine;

public class RotateToFaceTarget : MonoBehaviour
{
    public AttackActions attack;
    public EntityAIBase entityAI;

    private void Awake()
    {
        SetupComponents();
    }

    private void SetupComponents()
    {
        // Weapon
        attack = GetComponentInChildren<AttackActions>();
        
        // AI Script
        entityAI = GetComponentInParent<EntityAIBase>();
    }

    private void Update()
    {
        // Rotate Weapon to Face Target if It Exists
        if (entityAI.currentTarget == null) {return;}
        
        attack.transform.rotation = RangedComponent.GetAimRotation(
            attack.transform.position,
            entityAI.currentTarget.transform.position);
    }
}