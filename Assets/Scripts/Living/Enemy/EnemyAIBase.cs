 using System;
 using UnityEngine;

 public abstract class EnemyAIBase : MonoBehaviour
 {
     // Assigned in Inspector
     
     
     // External Objects Necessary
     protected EnemyAttackBase attackScript;
     protected EnemyMovementBase movementScript;
     
     // Variables used in Script
     public string pursuingTargetTag;
     public EnemyState currentState;
     
     // Current Target reference
     public GameObject _currentTarget;
     
     // When Current Target becomes Null, Find New Target
     public GameObject currentTarget
     {
         get => _currentTarget;
         set
         {
             if (_currentTarget != value)
             {
                 _currentTarget = value;

                 if (ShouldChangeCurrentTarget(_currentTarget))
                 {
                     SetNewTarget();
                 }
             }
         }
     }
     
     // The condition under which the Current Target Should Be Replaced 
     protected virtual bool ShouldChangeCurrentTarget(GameObject newCurrentValue)
     {
         return (newCurrentValue == null);
     }
     
     protected virtual void Start()
     {
         SetupComponents();
         
         SetNewTarget();
     }

     protected virtual void Update()
     {
         // Set Current State to Attacking if Near Enemy, or Pursuing if Not
         if(!attackScript.executingAttack)
            currentState = NearbyTarget() ? EnemyState.Attacking : EnemyState.Pursuing;
     }

     protected virtual void SetupComponents()
     {
         // Attack Base Script
         if (attackScript == null)
             attackScript = GetComponent<EnemyAttackBase>();
         
         // Movement Base Script
         if (movementScript == null)
             movementScript = GetComponent<EnemyMovementBase>();
     }
     
     // Replace Current Target
     public void SetNewTarget(GameObject target = null)
     {
         currentTarget = target != null ? target : FindNewTarget();
     }
     
     // Find New Target
     protected virtual GameObject FindNewTarget()
     {
         return FilterAppropriateTargets(GameObject.FindGameObjectsWithTag(pursuingTargetTag));
     }

     protected virtual GameObject FilterAppropriateTargets(GameObject[] targets)
     {
         return targets[0];
     }

     // Check if Current Target is within Attack Distance
     public bool NearbyTarget()
     {
         var distanceToTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
         return  distanceToTarget <= attackScript.enemyAttackScript.attackDistance;
     }
 }
