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
     public bool stateLocked;

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
         // Set Current State to Attacking if Near Enemy, or Pursuing if Not
         if(NearbyTarget())
             RequestStateChange(EnemyState.Attacking);
         else
             RequestStateChange(EnemyState.Pursuing);
         
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
     
     // Replace Current Target
     public virtual void SetNewTarget(GameObject target = null)
     {
         currentTarget = target != null ? target : FindNewTarget();
         
         currentTargetCollider = currentTarget.GetComponent<Collider2D>();
     }
     
     // Find New Target
     protected virtual GameObject FindNewTarget()
     {
         return GameObject.FindGameObjectWithTag(pursuingTargetTag);
     }


     public void RequestStateChange(EnemyState newState)
     {
         if(stateLocked) {return;}
         
         currentState = newState;
     }

     public void LockState(float duration)
     {
         Utils.ToggleBoolInTime(v => stateLocked = v, stateLocked, duration);
     }

     // Check if Current Target is within Attack Distance
     public bool NearbyTarget()
     {
         var distanceToTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
         return  distanceToTarget <= attackScript.actionHandler.actionRange || enemyRb.IsTouching(currentTargetCollider);
     }
 }
