using System;
using UnityEngine;

public class RotateToFaceTarget : MonoBehaviour
{
    public AttackActions attack;
    public EnemyAIBase enemyAI;

    private void Awake()
    {
        SetupComponents();
    }

    private void SetupComponents()
    {
        // Weapon
        attack = GetComponentInChildren<AttackActions>();
        
        // AI Script
        enemyAI = GetComponentInParent<EnemyAIBase>();
    }

    private void Update()
    {
        // Rotate Weapon to Face Target if It Exists
        if (enemyAI.currentTarget == null) {return;}
        
        attack.transform.rotation = ShootingComponent.GetAimRotation(
            attack.transform.position,
            enemyAI.currentTarget.transform.position);
    }
}