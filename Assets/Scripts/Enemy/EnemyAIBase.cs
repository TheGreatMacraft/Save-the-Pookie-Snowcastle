using UnityEngine;

 public abstract class EnemyAIBase : MonoBehaviour
 {
     // External Objects Necessary
     protected EnemyAttackBase attackScript;
     protected EnemyMovementBase movementScript;
     
     protected Rigidbody2D enemyRb;
     protected Collider2D currentTargetCollider;
     
     // Variables used in Script
     public string pursuingTargetTag;
     
     [field: SerializeField]
     public EnemyState currentState { get; private set; }

     public GameObject currentTarget;
     
     // The condition under which the Current Target Should Be Replaced 
     protected virtual bool ShouldChangeCurrentTarget(GameObject value)
     {
         return (value == null);
     }
     
     protected virtual void Start()
     {
         SetupComponents();
         
         SetNewTarget();
     }

     protected virtual void Update()
     {
         if(attackScript.attackAction.canAct)
            UpdateCurrentState();
         
         if(ShouldChangeCurrentTarget(currentTarget))
             SetNewTarget();
     }
     
     protected virtual void SetupComponents()
     {
         // Attack Base Script
         if (attackScript == null)
             attackScript = GetComponent<EnemyAttackBase>();
         
         // Movement Base Script
         if (movementScript == null)
             movementScript = GetComponent<EnemyMovementBase>();
         
         // Rigidbody
         if (enemyRb == null)
             enemyRb = GetComponent<Rigidbody2D>();
     }

     public void UpdateCurrentState()
     {
         // Set Current State to Attacking if Near Enemy, or Pursuing if Not
         currentState = NearbyTarget() ? EnemyState.Attacking : EnemyState.Pursuing;
     }
     
     // Replace Current Target
     public virtual void SetNewTarget(GameObject target = null)
     {
         currentTarget = target != null ? target : FindNewTarget();
         
         if(currentTarget != null)
            currentTargetCollider = currentTarget.GetComponent<Collider2D>();
     }
     
     // Find New Target
     protected virtual GameObject FindNewTarget()
     {
         return GameObject.FindGameObjectWithTag(pursuingTargetTag);
     }
     
     // Check if Current Target is within Attack Distance
     public bool NearbyTarget()
     {
         var distanceToTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
         return  distanceToTarget <= attackScript.actionHandler.actionRange || enemyRb.IsTouching(currentTargetCollider);
     }
 }
